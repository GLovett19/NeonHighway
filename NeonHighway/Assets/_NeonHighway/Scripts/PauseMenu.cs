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
    public SteamVR_Input_Sources DominantHand;
    public SteamVR_Action_Boolean ClickUpperButton;
    public SteamVR_Action_Boolean TriggerClick;
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
            //Debug.Log("count:" + counter);
        }
        else if (ClickUpperButton.GetState(SteamVR_Input_Sources.RightHand) && !RightHand.grabPoser)
        {
            counter += Time.deltaTime;
            //Debug.Log("count:" + counter);
        }
        if (ClickUpperButton.GetStateUp(SteamVR_Input_Sources.LeftHand) && !LeftHand.grabPoser)
        {
                counter = 0;
        }
        else if (ClickUpperButton.GetStateUp(SteamVR_Input_Sources.RightHand) && !RightHand.grabPoser)
        {
            counter = 0;
        }

        if (counter >= openTime)
        {
            counter = 0;
            isVisible = true;
            myMenu.ShowPanel(myMenu.FirstPanel.name);
            // enable dominant hand pointer
            if (DominantHand == SteamVR_Input_Sources.RightHand)
            {
                RightHand.GetComponentInChildren<pointer>().SetLineVisibility(true);
                myMenu.GetComponent<Canvas>().worldCamera = RightHand.GetComponentInChildren<Camera>();
            }
            else
            {
                LeftHand.GetComponentInChildren<pointer>().SetLineVisibility(true);
                myMenu.GetComponent<Canvas>().worldCamera = LeftHand.GetComponentInChildren<Camera>();
            }
            // set canvas event camera to dominant hand pointer camera 
            


        }
        if (!isVisible)
        {
            transform.position = cam.position;
            Vector3 v = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(v.x, cam.transform.rotation.eulerAngles.y, v.z);
        }
        else
        {
            if (TriggerClick.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Debug.Log("LeftHandDominant");
                // set left handdominant
                DominantHand = SteamVR_Input_Sources.LeftHand;
                RightHand.GetComponentInChildren<pointer>().SetLineVisibility(false);
                LeftHand.GetComponentInChildren<pointer>().SetLineVisibility(true);
                myMenu.GetComponent<Canvas>().worldCamera = LeftHand.GetComponentInChildren<Camera>();
            }
            if (TriggerClick.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("LeftHandDominant");
                // set right hand dominant
                DominantHand = SteamVR_Input_Sources.RightHand;
                RightHand.GetComponentInChildren<pointer>().SetLineVisibility(true);
                LeftHand.GetComponentInChildren<pointer>().SetLineVisibility(false);
                myMenu.GetComponent<Canvas>().worldCamera = RightHand.GetComponentInChildren<Camera>();
            }
        }
    }

    public void SetVisible(bool val)
    {
        isVisible = val;
        if (!val)
        {
            RightHand.GetComponentInChildren<pointer>().SetLineVisibility(false);
            LeftHand.GetComponentInChildren<pointer>().SetLineVisibility(false);
        }
    }
}
