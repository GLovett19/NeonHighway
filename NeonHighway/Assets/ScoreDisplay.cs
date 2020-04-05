using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // publc components 
    public MenuGeneric myMenuManager;

    //public fields 
    public int displayNum; // how many scored to display on the board at one time 
    public Text displayText; // the place where the text it displayed


    private int levelIndex = 0; 
    public SaveDataScript.ScoreData newScoreData; 

    public void Start()
    {
        levelIndex = 0;
        myMenuManager = GetComponentInParent<MenuGeneric>();

        SaveDataScript.Load();
        //SaveDataScript.MySaveData.scoreDataset.Sort(SortFunc);
        foreach (SaveDataScript.LevelScoreData levelData in SaveDataScript.MySaveData.levelScoreDataSet)
        {
            if (SaveDataScript.MySaveData.LastCompletedLevel == levelData.LevelName)
            {
                levelData.scoreDataset.Sort(SortFunc);
                //SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset.Sort(SortFunc);
            }
            else
            {
                levelIndex++;
            }
        }


        //scoreDataset.Sort(SortFunc);
        try
        {
            newScoreData.scoreValue = FindObjectOfType<AppManager>().scorePasser;
        }
        catch
        {
            Debug.Log("Couldnt find Preload Scene, using Test Score");
            newScoreData.scoreValue = SaveDataScript.MySaveData.levelScoreDataSet[levelIndex].scoreDataset[0].scoreValue + 1;
        }

        //if (newScoreData.scoreValue > scoreDataset[scoreDataset.Count - 1].scoreValue)
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
