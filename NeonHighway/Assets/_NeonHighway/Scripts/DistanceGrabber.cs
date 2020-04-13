using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrabber : MonoBehaviour
{
    public float defaultLength = 5.0f;
    public GameObject dot;

    Vector3 distanceSelectedPosition = Vector3.zero;
    private LineRenderer line = null;
    private CustomHand hand;
    private Collider[] SelectedDistanceGripColliders;
    public CustomInteractible LastSelectedInteractabble;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        hand = GetComponentInParent<CustomHand>();
    }
    private void Update()
    {
        UpdateLine();
    }

    private void FixedUpdate()
    {
        SelectDistanceGribObject();
    }

    private void UpdateLine()
    {
        // use default value for length or distance 
        float targetLength = defaultLength;

        // raycast
        RaycastHit hit = CreateRaycast(targetLength);


        //default end 
        Vector3 endPosition = transform.position + (transform.forward * targetLength);


        // or based on hid
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }


        // set position of dot
        dot.transform.position = endPosition;


        //

        if (LastSelectedInteractabble && !hand.grabPoser)
        {
            line.enabled = true;
            //set position of line renderer
            line.SetPosition(0, transform.position);
            line.SetPosition(1, LastSelectedInteractabble.transform.position);
        }
        else
        {
            line.enabled = false;
            //set position of line renderer
            line.SetPosition(0, transform.position);
            line.SetPosition(1, endPosition);
        }
        
        
    }

    private void SelectDistanceGribObject()
    {
        LastSelectedInteractabble = null;
        if (!hand.grabPoser && hand.SelectedGpibColliders.Length == 0)
        {

            SelectedDistanceGripColliders = Physics.OverlapSphere(dot.transform.position, hand.gripRadius, hand.layerColliderChecker);
            hand.SelectedGpibInteractible = null;
            float tempCloseDistance = float.MaxValue;
            for (int i = 0; i < SelectedDistanceGripColliders.Length; i++)
            {
                CustomInteractible tempCustomInteractible = SelectedDistanceGripColliders[i].GetComponentInParent<CustomInteractible>();
                if (tempCustomInteractible != null && tempCustomInteractible.isInteractible && tempCustomInteractible.grabType == CustomHand.GrabType.Grip && tempCustomInteractible.DistanceGrabbable)
                {
                    if (Vector3.Distance(tempCustomInteractible.transform.position, dot.transform.position) < tempCloseDistance)
                    {
                        tempCloseDistance = Vector3.Distance(tempCustomInteractible.transform.position, dot.transform.position);
                        hand.SelectedGpibInteractible = tempCustomInteractible;
                        LastSelectedInteractabble = tempCustomInteractible;
                    }
                }

            }
        }
        else
        {
            LastSelectedInteractabble = null;
        }

    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);


        return hit;
    }
}
