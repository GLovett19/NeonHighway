using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCountDown : MonoBehaviour
{
    
    //Components 
    public Text CountDowntext;
    //public Fields

    //Private Fields
    float f_Counter;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (f_Counter > 0)
        {
            CountDowntext.enabled = true;
            switch((int)f_Counter)
            {
                case 4:
                    CountDowntext.text = "3";
                break;
                case 3:
                    CountDowntext.text = "2";
                    break;
                case 2:
                    CountDowntext.text = "1";
                    break;
                case 1:
                    CountDowntext.text = "GO";                   
                    break;
                case 0:
                    
                    f_Counter = 0;
                    
                    CountDowntext.enabled = false;
                    break;
                default:
                    break;
            }
            f_Counter -= Time.deltaTime;
        }

        
    }
    public void SetCounter(float val)
    {
        f_Counter = val;
    }

}
