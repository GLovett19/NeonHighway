using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingButtons : MonoBehaviour
{
    public List<GameObject> buttons;

    public int selectedIndex;
    public float count;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // move the selected button to the center of the view
        // make this button bigger than the others by scaling it up a bit. 
        int i = 0;
        foreach (GameObject obj in buttons)
        {
            if (i != selectedIndex)
            {
                obj.GetComponent<Image>().color = new Color(obj.GetComponent<Image>().color.r, obj.GetComponent<Image>().color.g, obj.GetComponent<Image>().color.b, 0.5f);
            }
            else
            {
                obj.GetComponent<Image>().color = new Color(obj.GetComponent<Image>().color.r, obj.GetComponent<Image>().color.g, obj.GetComponent<Image>().color.b, 1f);
            }
            i++;
        }






        // counting stuff not important
        if (count > 0)
        {
            count -= Time.deltaTime;
        }
        else
        {
            count = 2;
            if (selectedIndex >= 4)
            {
                selectedIndex = 0;
            }
            else
            {
                selectedIndex++;
            }
        }

    }
}
