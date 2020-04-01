using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class StaticMenuHandler : MonoBehaviour
{

    /// <summary>
    /// This menu handler controls menus which remain 
    /// stationairy and are allways open. 
    /// 
    /// 
    /// </summary>
    /// 

    // Inspector assigned Components
    [Header("Inspector Assigned Components")]
    public SteamVR_Action_Boolean TriggerClick;
    public CustomHand LeftHand, RightHand;

    // self assigned components
    private MenuGeneric myMenu;

    [Header("Public Fields")]
    // public Fields 
    //public SteamVR_Input_Sources handType;
    public SteamVR_Input_Sources DominantHand;



    float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        myMenu = GetComponentInChildren<MenuGeneric>();
        myMenu.ShowPanel(myMenu.FirstPanel.name);
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
    }

    // Update is called once per frame
    void Update()
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
