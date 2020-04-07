using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DynamicMenuHandler : MonoBehaviour
{
    /// <summary>
    /// This menu handler controls menus which should 
    /// move with the player and be opened and closed 
    /// at the players discretion 
    /// 
    /// </summary>
    /// 

    // Inspector assigned Components
    [Header("Inspector Assigned Components")]
    public Transform cam;
    public SteamVR_Action_Boolean ClickUpperButton, TriggerClick;
    public CustomHand LeftHand, RightHand;

    // self assigned components
    private MenuGeneric myMenu;

    [Header("Public Fields")]
    // public Fields 
    //public SteamVR_Input_Sources handType;
    public SteamVR_Input_Sources DominantHand;
    public float openTime = 1;
 

    //private Fields
    private bool isVisible;



    float counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        myMenu = GetComponentInChildren<MenuGeneric>();
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
            myMenu.ShowSinglePanel(myMenu.FirstPanel.name);
            // enable dominant hand pointer
            if (DominantHand == SteamVR_Input_Sources.RightHand)
            {
                RightHand.GetComponentInChildren<Pointer>().SetLineVisibility(true);
                myMenu.GetComponent<Canvas>().worldCamera = RightHand.GetComponentInChildren<Camera>();
            }
            else
            {
                LeftHand.GetComponentInChildren<Pointer>().SetLineVisibility(true);
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
                RightHand.GetComponentInChildren<Pointer>().SetLineVisibility(false);
                LeftHand.GetComponentInChildren<Pointer>().SetLineVisibility(true);
                myMenu.GetComponent<Canvas>().worldCamera = LeftHand.GetComponentInChildren<Camera>();
            }
            if (TriggerClick.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("LeftHandDominant");
                // set right hand dominant
                DominantHand = SteamVR_Input_Sources.RightHand;
                RightHand.GetComponentInChildren<Pointer>().SetLineVisibility(true);
                LeftHand.GetComponentInChildren<Pointer>().SetLineVisibility(false);
                myMenu.GetComponent<Canvas>().worldCamera = RightHand.GetComponentInChildren<Camera>();
            }
        }
    }

    public void SetVisible(bool val)
    {
        isVisible = val;
        if (!val)
        {
            RightHand.GetComponentInChildren<Pointer>().SetLineVisibility(false);
            LeftHand.GetComponentInChildren<Pointer>().SetLineVisibility(false);
        }
    }
}
