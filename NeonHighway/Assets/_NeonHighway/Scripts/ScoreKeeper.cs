using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    // public componente inspector assigned
    public Text text;
    // private components auto assigned 

    //public fields 

    // private fields 
    int playerScore;

    public void Update()
    {
        text.text = "Score: " + playerScore;
    }

    public void SetPlayerScore(int val)
    {
        playerScore = val;
    }
    public void AddPlayerScore(int val,Transform location)
    {
        playerScore += val;
        //CreatePopupText(val.ToString(), location);
    }
    public int GetPlayerScore()
    {
        return playerScore;
    }
  
}
