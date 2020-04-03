using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public MenuGeneric myMenuManager;

    public int displayNum;
    public Text displayText;

    public SaveDataScript.ScoreData newScoreData; 

    public void Start()
    {
        myMenuManager = GetComponentInParent<MenuGeneric>();

        SaveDataScript.Load();
        SaveDataScript.MySaveData.scoreDataset.Sort(SortFunc);
        //scoreDataset.Sort(SortFunc);
        try
        {
            newScoreData.scoreValue = FindObjectOfType<AppManager>().scorePasser;
        }
        catch
        {
            Debug.Log("Couldnt find Preload Scene, using Test Score");
            newScoreData.scoreValue = SaveDataScript.MySaveData.scoreDataset[0].scoreValue + 1;
        }

        //if (newScoreData.scoreValue > scoreDataset[scoreDataset.Count - 1].scoreValue)
        if (SaveDataScript.MySaveData.scoreDataset.Count >= displayNum)
        {
            if (newScoreData.scoreValue > SaveDataScript.MySaveData.scoreDataset[SaveDataScript.MySaveData.scoreDataset.Count - 1].scoreValue)
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
            if (i < SaveDataScript.MySaveData.scoreDataset.Count)
            {
                str += (i.ToString() + " ): " + SaveDataScript.MySaveData.scoreDataset[i].scoreName + " : " + SaveDataScript.MySaveData.scoreDataset[i].scoreValue.ToString() + "\n");
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
        SaveDataScript.MySaveData.scoreDataset.Add(newScoreData);
        SaveDataScript.MySaveData.scoreDataset.Sort(SortFunc);

        displayText.text = DisplayScores();
        myMenuManager.HidePanel("CharacterInput");

        SaveDataScript.Save();
    }


}
