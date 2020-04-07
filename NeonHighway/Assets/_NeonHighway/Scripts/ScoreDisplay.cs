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
    private int levelIndex = 0;

    

    public void Start()
    {
       
        myMenuManager = GetComponentInParent<MenuGeneric>();

        SaveDataScript.Load();

        // find the last completed level and get its index when completing a level
        if (!DisplayOnly)
        {
            levelIndex = GetLevelIndex(SaveDataScript.MySaveData.LastCompletedLevel);
        }
        else 
        {
            // get index from current menu selection? 
        }

        //sort the level's data set
        SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Sort(SortFunc);


        if (!DisplayOnly)
        {
            CheckAddScore();
        }
        displayText.text = DisplayScores();
        
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
        return temp;
    }

    public string DisplayScores()
    {
        string str = "";
        for (int i = 0; i < displayNum; i++)
        {
            Debug.Log(levelIndex);
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

    public void SetLevelIndex(string val)
    {
        levelIndex = GetLevelIndex(val);
    }

    public void CheckAddScore()
    {
       

        // get the score from the previously completed level
        try
        {
            newScoreData.scoreValue = FindObjectOfType<AppManager>().scorePasser;
        }
        catch
        {
            Debug.Log("Couldnt find Preload Scene app manager, using Test Score");
            newScoreData.scoreValue = SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[0].scoreValue + 1;
        }

        //check if the scoreboard is full andwether or not the player should be allowed to add their score to the board. 
        if (SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count >= displayNum)
        {
            if (newScoreData.scoreValue > SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Count - 1].scoreValue)
            {
                myMenuManager.ShowPanel("CharacterInput");
            }
        }
        else
        {
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


}
