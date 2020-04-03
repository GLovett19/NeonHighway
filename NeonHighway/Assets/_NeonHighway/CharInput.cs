using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharInput : MonoBehaviour
{

    // ascii character are 65 - 90 for uppercase & 97 - 122 for lowercase
    public Text CharDisplay;
    public Text NameDisplay;
    public int asciiValue = 65;

    public bool cursorBlink;   
    public float cursorBlinkSpeed;
    public string myName;

    private string cursorCharacter = "";
    private bool cursorOn = false;
    private float counter = 0;
    private void Update()
    {
        CharDisplay.text = System.Convert.ToChar(asciiValue).ToString();
        if (cursorBlink)
        {
            if (counter > cursorBlinkSpeed)
            {
                counter = 0;
                if (!cursorOn)
                {
                    cursorOn = true;
                    if (myName.Length < 3)
                    {
                        cursorCharacter = "_";
                    }
                }
                else
                {
                    cursorOn = false;
                    if (cursorCharacter.Length != 0)
                    {
                        cursorCharacter = cursorCharacter.Substring(0, cursorCharacter.Length - 1);
                    }
                }


               
            }
            else
            {
                counter +=Time.deltaTime;
            }          
        }
        NameDisplay.text = myName + cursorCharacter;
    }

    public void IncrementValue()
    {
        // clicked the upper button 
        if (asciiValue < 90)
        {
            asciiValue++;
        }
        else
        {
            asciiValue = 65;
        }
    }
    public void DecrementValue()
    {
        // clicked the lower button
        if (asciiValue > 65)
        {
            asciiValue--;
        }
        else
        {
            asciiValue = 90;
        }
    }
    public void SubmitChar()
    {
        // assign this character to the high score table name 
        if (myName.Length < 3)
        {
                myName += System.Convert.ToChar(asciiValue).ToString();

        }
        else
        {
            // submit this name and score to the scoreboard and close the input menu.
        }
    }
    public void CancelChar()
    {
        if (myName.Length > 0)
        {
                myName = myName.Substring(0, myName.Length - 1);
        }

    }
}
