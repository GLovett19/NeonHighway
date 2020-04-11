using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // publc components 
    public Text displayText; // the place where the text it displayed

    //private Components
    private MenuGeneric myMenuManager;

    //public fields 
    public bool DisplayOnly; // this means the menu is only allowed to display scores not add to them. 
    public int displayNum; // how many scored to display on the board at one time 
    public SaveDataScript.ScoreData newScoreData;
    

   // private fields 
    private  int levelIndex = 0;
    private string selectedLevel;
    

    public void Start()
    {
       
        myMenuManager = GetComponentInParent<MenuGeneric>();

        SaveDataScript.Load();

        // find the last completed level and get its index when completing a level
        if (!DisplayOnly)            
        {
            //Debug.Log("x");
            levelIndex = GetLevelIndex(SaveDataScript.MySaveData.LastCompletedLevel);
            if (levelIndex < 0)
            {
                // create a new level dataset
                AddLevelDataset(SaveDataScript.MySaveData.LastCompletedLevel);
                levelIndex = GetLevelIndex(SaveDataScript.MySaveData.LastCompletedLevel);
            }

        }
        else 
        {
            levelIndex = GetLevelIndex(selectedLevel);
        }

        //sort the level's data set
        //Debug.Log(SaveDataScript.MySaveData.levelScoreDataSet.Count);
        if (levelIndex < SaveDataScript.MySaveData.levelScoreDataSet.Count && levelIndex >= 0)
        {
            //Debug.Log("y");
            SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Sort(SortFunc);


            if (!DisplayOnly)
            {
                //Debug.Log("Z");
                CheckAddScore();
            }
            displayText.text = DisplayScores();
        }
    }

    private int SortFunc(SaveDataScript.ScoreData a, SaveDataScript.ScoreData b)
    {
        if (a.scoreValue < b.scoreValue)
        {
            return 1;
        }
        else if (a.scoreValue > b.scoreValue)
        {
            return -1;
        }
        return 0;
    }

    public int GetLevelIndex(string name)
    {
        int temp = 0;
        foreach (SaveDataScript.LevelScoreData levelData in SaveDataScript.MySaveData.levelScoreDataSet)
        {
            if (name == levelData.LevelName)
            {
                break;
            }
            else
            {
                temp++;
            }
        }
        if (temp > SaveDataScript.MySaveData.levelScoreDataSet.Count-1)
        {
            return -1;
        }
        return temp;
    }

    public string DisplayScores()
    {
        string str = "";
        for (int i = 0; i < displayNum; i++)
        {
           // Debug.Log(levelIndex);
            if (i < SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count)
            {
                str += (i.ToString() + " ): " + SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[i].scoreName + " : " + SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[i].scoreValue.ToString() + "\n");
                //str += (i.ToString() + " ): " + scoreDataset[i].scoreName + " : " + scoreDataset[i].scoreValue.ToString() + "\n");
            }
            else
            {
                str += (i.ToString() + " ): " + " : " + "\n");
            }
        }
        return str;
    }

    public string GetSelectedlevel()
    {
        return selectedLevel;
    }
    public void SetLevelIndex(string val)
    {
        levelIndex = GetLevelIndex(val);
    }

    public void SetLevelString(string val)
    {
        // set the selected level string
        selectedLevel = val;
        
        // update the level index to match the selected string 
        levelIndex = GetLevelIndex(selectedLevel);

        // if this level has been completed at least once and has scores
        if (levelIndex < SaveDataScript.MySaveData.levelScoreDataSet.Count && levelIndex >= 0)
        {
            SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Sort(SortFunc);
            displayText.text = DisplayScores();
        }
        else
        {
            displayText.text = "No Scores";
        }
    }
    public void CheckAddScore()
    {
        //Debug.Log("A");

        // get the score from the previously completed level
        try
        {
            //Debug.Log("B1");
            newScoreData.scoreValue = FindObjectOfType<AppManager>().scorePasser;
        }
        catch
        {
            //Debug.Log("B2");
            Debug.Log("Couldnt find Preload Scene app manager, using Test Score");
            try
            {
                newScoreData.scoreValue = SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[0].scoreValue + 1;
            }
            catch
            {
                newScoreData.scoreValue = 1;
            }
        }

        //check if the scoreboard is full andwether or not the player should be allowed to add their score to the board. 
        if (SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count >= displayNum)
        {
           // Debug.Log("C1");
            if (newScoreData.scoreValue > SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count - 1].scoreValue)
            {
                //Debug.Log("D");
                myMenuManager.ShowPanel("CharacterInput");
            }
        }
        else
        {
           // Debug.Log("C2");
            myMenuManager.ShowPanel("CharacterInput");
        }
    }

    public void AddScore(string name)
    {
        //newScoreData.scoreName = name;
        //scoreDataset.Add(newScoreData);
        //scoreDataset.Sort(SortFunc);

        newScoreData.scoreName = name;
        SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Add(newScoreData);
        SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Sort(SortFunc);
        if (SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count > displayNum)
        {
            SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.RemoveRange(6, SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count - 6);
        }

        displayText.text = DisplayScores();
        myMenuManager.HidePanel("CharacterInput");

        SaveDataScript.Save();
    }
    public void AddLevelDataset(string name)
    {
        SaveDataScript.LevelScoreData newLevelScoreData = new SaveDataScript.LevelScoreData();
        newLevelScoreData.LevelName = name;
        SaveDataScript.MySaveData.levelScoreDataSet.Add(newLevelScoreData);
        SaveDataScript.Save();
    }


}
