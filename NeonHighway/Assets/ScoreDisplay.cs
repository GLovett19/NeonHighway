using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public int displayNum;


    public Text displayText;

    
    public List<SaveDataScript.ScoreData> scoreDataset;

    public void Start()
    {
        scoreDataset.Sort(SortFunc);
        
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
            str += ("1): " + scoreDataset[i].scoreName + " : " + scoreDataset[i].scoreValue.ToString() + "\n");
        }
        return str;
    }

}
