using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PauseMenu : MonoBehaviour
{
    
    public Transform cam;
    public bool isVisible;
    public float openTime = 1;
    public MenuGeneric myMenu;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean ClickUpperButton;
    public CustomHand LeftHand, RightHand;

    float counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if a button is clicked && that hand is not holding anything 
        if (ClickUpperButton.GetState(SteamVR_Input_Sources.LeftHand) && !LeftHand.grabPoser)
        {
            counter += Time.deltaTime;
            Debug.Log("count:" + counter);
        }
        else if (ClickUpperButton.GetState(SteamVR_Input_Sources.RightHand) && !RightHand.grabPoser)
        {
            counter += Time.deltaTime;
            Debug.Log("count:" + counter);
        }
        if (ClickUpperButton.GetStateUp(SteamVR_Input_Sources.LeftHand) && !LeftHand.grabPoser)
        {
                counter = 0;
        }
        if (ClickUpperButton.GetStateUp(SteamVR_Input_Sources.RightHand) && !RightHand.grabPoser)
        {
            counter = 0;
        }
        if (counter >= openTime)
        {
            counter = 0;
            isVisible = true;
            myMenu.ShowPanel(myMenu.FirstPanel.name);
        }
        if(!isVisible)
        {
        transform.position = cam.position;
        Vector3 v = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(v.x, cam.transform.rotation.eulerAngles.y, v.z);
        }
    }

    public void OpenMenuCheck(SteamVR_Input_Sources source)
    {
        if (ClickUpperButton.GetState(source) && !LeftHand.grabPoser)
        {
            counter += Time.deltaTime;
            Debug.Log("count:" + counter);
        }
        if (ClickUpperButton.GetStateUp(source) && !LeftHand.grabPoser)
        {
            counter = 0;
        }
    }
    public void SetVisible(bool val)
    {
        isVisible = val;
    }
}
