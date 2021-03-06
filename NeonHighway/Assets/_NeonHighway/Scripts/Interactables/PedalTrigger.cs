﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PedalTrigger : MonoBehaviour
{
    public float c_Axis,l_Axis,r_Axis;//button press %
    public Vector2 angle; //turn out limit
    public SteamVR_Action_Single triggerAxis = SteamVR_Input.GetAction<SteamVR_Action_Single>("Trigger"); //input   
    public CustomHand leftHand, rightHand;

    void Start()
    {
    }
    public void Update()
    {
        if (leftHand.grabType == CustomHand.GrabType.None || leftHand.grabPoser.GetComponentInParent<SteeringWheel>())
        {
            customUpdate(leftHand);
        }
        else
        {
            l_Axis = 0;
        }
        if (rightHand.grabType == CustomHand.GrabType.None || rightHand.grabPoser.GetComponentInParent<SteeringWheel>())
        {
            customUpdate(rightHand);
        }
        else
        {
            r_Axis = 0;
        }
        c_Axis = l_Axis + r_Axis;
    }

    public void customUpdate(CustomHand hand)
	{

        //Debug.Log(hand.handType); // returns LeftHand or RightHand
        switch (hand.handType.ToString())
        {
            case "LeftHand":
                l_Axis = triggerAxis.GetAxis(hand.handType);
                break;
            case "RightHand":
                r_Axis = triggerAxis.GetAxis(hand.handType) * -1;
                break;
            default:
            break;
        }
	}
}
