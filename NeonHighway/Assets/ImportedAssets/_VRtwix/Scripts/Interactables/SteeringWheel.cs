using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteeringWheel : CustomInteractible {
	public float angle,clamp;//steerwing wheel angle, rotation limit
	float angleLeft,angleRight; //angle from steering wheel to hands
	Vector2 oldPosLeft,oldPosRight; //old hands positions
	public Transform RotationObject; //moving object

	public float radius; //wheel radius
	bool ReversHand; //turn out hands, depending of interaction side 


	public bool PowerSteering;
	public float PowerSteeringStrength;
	bool PowerSteeringActive;

	// adding "foot pedals" with triggers
	//public PedalTrigger pedal;
	// end added Script

	void Start () {
		if (grabPoints!=null&&grabPoints.Count>0)
			radius = grabPoints [0].transform.localPosition.magnitude;
	}

	// adding power steering 
	
	private void Update()
	{
		// if not grabbed interpolate back to starting position
		if (PowerSteering && PowerSteeringActive)
		{
			if (angle > 0)
			{
				angle -= PowerSteeringStrength * Time.deltaTime;
			}
			if (angle < 0)
			{
				angle += PowerSteeringStrength * Time.deltaTime;
			}
			RotationObject.localEulerAngles = new Vector3(0, 0, angle);
		}
	}
	
	//
	public void GrabStart(CustomHand hand){
		// power steering stuff
		PowerSteeringActive = false;
		//
		SetInteractibleVariable (hand);
		hand.SkeletonUpdate ();
		hand.PivotUpdate ();
		Transform tempPoser=GetMyGrabPoserTransform (hand);
		Vector3 HandTolocalPos = transform.InverseTransformPoint (hand.PivotPoser.position);
		HandTolocalPos.z = 0;
		tempPoser.localPosition = HandTolocalPos;
		if (hand.handType == SteamVR_Input_Sources.LeftHand) {
			oldPosLeft = new Vector2 (HandTolocalPos.x, HandTolocalPos.y);
		} else {
			if (hand.handType == SteamVR_Input_Sources.RightHand) {
				oldPosRight = new Vector2 (HandTolocalPos.x, HandTolocalPos.y);
			} 
		}
		ReversHand = Vector3.Angle (transform.forward, hand.PivotPoser.forward) < 90;
		Grab.Invoke ();
	}

	public void GrabUpdate(CustomHand hand){

		// adding the grab custom update for the trigger
		//pedal.customUpdate(hand);
		// end added Script

		Transform tempPoser = GetMyGrabPoserTransform (hand);
		Vector3 HandTolocalPos = transform.InverseTransformPoint (hand.PivotPoser.position);
		HandTolocalPos.z = 0;
		tempPoser.localPosition = HandTolocalPos;


		if (hand.handType == SteamVR_Input_Sources.LeftHand) {
				angle-=Vector2.SignedAngle (tempPoser.localPosition, oldPosLeft)*(leftHand&&rightHand?leftHand.Squeeze==rightHand.Squeeze?.5f:hand.Squeeze/(Mathf.Epsilon+(leftHand.Squeeze+rightHand.Squeeze)):1f);
			
			oldPosLeft = new Vector2 (HandTolocalPos.x, HandTolocalPos.y);
		} else {
			if (hand.handType == SteamVR_Input_Sources.RightHand) {
					angle-=Vector2.SignedAngle (tempPoser.localPosition, oldPosRight)*(leftHand&&rightHand?leftHand.Squeeze==rightHand.Squeeze?.5f:hand.Squeeze/(Mathf.Epsilon+(leftHand.Squeeze+rightHand.Squeeze)):1f);
				
				oldPosRight = new Vector2 (HandTolocalPos.x, HandTolocalPos.y);
			} 
		}
		angle = Mathf.Clamp (angle, -clamp, clamp);
		RotationObject.localEulerAngles=new Vector3 (0, 0, angle);
		tempPoser.localPosition = tempPoser.localPosition.normalized * radius;
		tempPoser.rotation = Quaternion.LookRotation (ReversHand? transform.forward:-transform.forward, tempPoser.position-transform.position);

	}

	public void GrabEnd(CustomHand hand){
		// powerSteering stuff 
		PowerSteeringActive = true;
		//
		DettachHand (hand);
		ReleaseHand.Invoke ();
	}
}
