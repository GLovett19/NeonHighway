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
    /// eventually all of the stuff determining dominant hand and line renderer shoudl be moved to the steam input module 
    /// </summary>
    /// 

    // Inspector assigned Components
    [Header("Inspector Assigned Components")]
    public SteamVR_Action_Boolean TriggerClick;

    // self assigned components
    private MenuGeneric myMenu;




    //float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        myMenu = GetComponentInChildren<MenuGeneric>();
        myMenu.ShowSinglePanel(myMenu.FirstPanel.name);
    }

    // Update is called once per frame
    void Update()
    {
        myMenu.GetInputModule().pointer.SetLineVisibility(true);
    }
}
