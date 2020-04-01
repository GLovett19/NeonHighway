using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;


public class VRInputModule : BaseInputModule
{   // public components
    [Header("Inspector Assigned Components")]
    public Camera cam;
    public SteamVR_Action_Boolean clickButton;

    // private components
    private GameObject currentObject = null;
    private PointerEventData data = null;

    //public fields 
    [Header("Public fields")]
    public SteamVR_Input_Sources handType;

    //private fields 
     // none


    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }
    public override void Process()
    {
        //reset data
        data.Reset();

        //set camera
        data.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        //Raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        // clear raycast
        m_RaycastResultCache.Clear();

        //handle hover state
        HandlePointerExitAndEnter(data, currentObject);


        //press
        if (clickButton.GetStateDown(handType))
            ProcessPress(data);

        //release
        if (clickButton.GetStateUp(handType))
            ProcessRelease(data);

    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {
        //set raycast 
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        //check for object hit, get the downhandler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        //if no downhandler try to get click handler
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        //set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;


    }

    private void ProcessRelease(PointerEventData data)
    {
        //Eecute pointer up 
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler 
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        //check if actual  click
        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear selected game obejct 
        eventSystem.SetSelectedGameObject(null);

        //reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;



    }
}
