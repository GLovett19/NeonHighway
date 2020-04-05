using System.Collections;
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
        if (!leftHand.grabPoser || leftHand.grabPoser.GetComponentInParent<SteeringWheel>())
        {
            customUpdate(leftHand);           
        }
        if(!rightHand.grabPoser || rightHand.grabPoser.GetComponentInParent<SteeringWheel>())
        {
            customUpdate(rightHand);
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
