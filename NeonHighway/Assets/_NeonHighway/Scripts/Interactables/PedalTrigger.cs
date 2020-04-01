using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PedalTrigger : MonoBehaviour
{
    public float Axis;//button press %
    public Vector2 angle; //turn out limit
    public SteamVR_Action_Single triggerAxis = SteamVR_Input.GetAction<SteamVR_Action_Single>("Trigger"); //input
    public SteamVR_Action_Boolean triggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TriggerClick"); //input

    public SteeringWheel steeringWheel;

    void Start()
    {
        //steeringWheel = GetComponentInParent<SteeringWheel>();
    }


	public void customUpdate(CustomHand hand)
	{

        Debug.Log(hand.handType); // returns LeftHand or RightHand
        switch (hand.handType.ToString())
        {
            case "LeftHand":
                Axis = triggerAxis.GetAxis(hand.handType);
                break;
            case "RightHand":
                Axis = triggerAxis.GetAxis(hand.handType) * -1;
                break;
            default:
            break;
        }

		

		transform.localEulerAngles = new Vector3(Mathf.Lerp(angle.x, angle.y, Axis), 0);
	}
}
