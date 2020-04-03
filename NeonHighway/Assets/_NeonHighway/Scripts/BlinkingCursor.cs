using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingCursor : MonoBehaviour
{
    public Text enteredString;

    private float m_TimeStamp;
    private bool cursor = false;
    private string cursorChar = "";
    private int maxStringLength = 3;
    

    void update()
    {
        if (Time.time - m_TimeStamp >= 0.5)
        {
            m_TimeStamp = Time.time;
            if (cursor == false)
            {
                cursor = true;
                if (enteredString.text.Length < maxStringLength)
                {
                    cursorChar += "_";
                    enteredString.text += cursorChar;
                }
            }
            else
            {
                cursor = false;
                if (cursorChar.Length != 0)
                {
                    cursorChar = cursorChar.Substring(0, cursorChar.Length - 1);
                    enteredString.text += cursorChar;
                }
            }
        }
       
    }
}
