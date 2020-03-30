using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrab : CustomHand
{
    /// <summary>
    /// This script does the following 
    /// 
    /// Generates a field of equidistance points in a circle 
    /// Raycasts from its origin to each of the generated points. 
    /// Adds any interactale physics objects to a list to be picked up
    /// highlights the object closest to the center of the cone
    /// 
    /// when the player grabs and pulls their hand back the object flies into their hand 
    /// 
    /// 
    /// Notes
    ///    Right now you point the controller at the object like a remote, I would rather 
    ///    have the points come out of the palm of your hand meaning the direction has to be changed.
    /// 
    /// </summary>

    //Public Components
    public LineRenderer SelectionLine;


    //Public fields 
    public int numPoints = 100;
    public float coneHeight = 10;
    public Vector3 coneRotation;
    [HideInInspector]
    public Vector3[] points;
    public LayerMask mask;

    //Private fields 
    float pow = 0.5f;


    public void PopulateDistanceGrabSelection(List<Collider> colList)
    {
        // Raycast out for each point on the distance grabber;
        for (int i = 0; i < numPoints; i++)
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (transform.TransformPoint(points[i]) - transform.position).normalized, out hit, 5f, mask, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.GetComponentInParent<CustomInteractible>())
                {
                    colList.Add(hit.collider);
                }
            }
            Debug.DrawLine(transform.position, hit.point,Color.yellow);
        }
    }

    public override void GrabCheck()
    {
        if (grabType != GrabType.None && GrabInteractible)
        {
            SelectionLine.enabled = false;
        }
            base.GrabCheck();
    }
    public override void SelectGribObject()
    {
        SelectionLine.enabled = false;
        List<Collider> SelectedDistanceGripColliders = new List<Collider>();
        if (!grabPoser)
        {
            
            SelectedGpibColliders = Physics.OverlapSphere(GrabPoint(), gripRadius, layerColliderChecker);
            PopulateDistanceGrabSelection(SelectedDistanceGripColliders);
            SelectedGpibInteractible = null;
            float tempCloseDistance = float.MaxValue;
            if (SelectedGpibColliders.Length > 0)
            {             
                for (int i = 0; i < SelectedGpibColliders.Length; i++)
                {
                    CustomInteractible tempCustomInteractible = SelectedGpibColliders[i].GetComponentInParent<CustomInteractible>();
                    if (tempCustomInteractible != null && tempCustomInteractible.isInteractible && tempCustomInteractible.grabType == GrabType.Grip)
                    {
                        if (Vector3.Distance(tempCustomInteractible.transform.position, GrabPoint()) < tempCloseDistance)
                        {
                            tempCloseDistance = Vector3.Distance(tempCustomInteractible.transform.position, GrabPoint());
                            SelectedGpibInteractible = tempCustomInteractible;
                        }
                    }
                }
            }
            else if (SelectedDistanceGripColliders.Count > 0)
            {
                SelectionLine.enabled = true;
                RaycastHit hit;
                Physics.Raycast(transform.position, (transform.TransformPoint(points[0]) - transform.position).normalized, out hit, 5f, mask, QueryTriggerInteraction.Ignore);
                for (int i = 0; i < SelectedDistanceGripColliders.Count; i++)
                {
                    CustomInteractible tempCustomInteractible = SelectedDistanceGripColliders[i].GetComponentInParent<CustomInteractible>();
                    if (tempCustomInteractible != null && tempCustomInteractible.isInteractible && tempCustomInteractible.grabType == GrabType.Grip)
                    {
                        if (Vector3.Distance(tempCustomInteractible.transform.position,hit.point) < tempCloseDistance)
                        {
                            tempCloseDistance = Vector3.Distance(tempCustomInteractible.transform.position, GrabPoint());
                            SelectedGpibInteractible = tempCustomInteractible;
                            SelectionLine.SetPosition(0, transform.position);
                            SelectionLine.SetPosition(1, SelectedGpibInteractible.transform.position);
                        }
                    }
                }
            }
        }

    }
    public override void SelectPinchObject()
    {
        SelectionLine.enabled = false;
        List<Collider> SelectedDistancePinchColliders = new List<Collider>();
        if (!grabPoser)
        {
            SelectedPinchColliders = Physics.OverlapSphere(PinchPoint(), pinchRadius, layerColliderChecker);
            PopulateDistanceGrabSelection(SelectedDistancePinchColliders);
            SelectedPinchInteractible = null;
            float tempCloseDistance = float.MaxValue;
            if (SelectedPinchColliders.Length > 0)
            {
                SelectionLine.enabled = false;
                for (int i = 0; i < SelectedPinchColliders.Length; i++)
                {
                    CustomInteractible tempCustomInteractible = SelectedPinchColliders[i].GetComponentInParent<CustomInteractible>();
                    if (tempCustomInteractible != null && tempCustomInteractible.isInteractible && tempCustomInteractible.grabType == GrabType.Pinch)
                    {
                        if (Vector3.Distance(tempCustomInteractible.transform.position, PinchPoint()) < tempCloseDistance)
                        {
                            tempCloseDistance = Vector3.Distance(tempCustomInteractible.transform.position, PinchPoint());
                            SelectedPinchInteractible = tempCustomInteractible;
                        }
                    }
                }
            }
            else if (SelectedDistancePinchColliders.Count > 0)
            {
                SelectionLine.enabled = true;
                RaycastHit hit;
                Physics.Raycast(transform.position, (transform.TransformPoint(points[0]) - transform.position).normalized, out hit, 5f, mask, QueryTriggerInteraction.Ignore);
                for (int i = 0; i < SelectedDistancePinchColliders.Count; i++)
                {
                    CustomInteractible tempCustomInteractible = SelectedDistancePinchColliders[i].GetComponentInParent<CustomInteractible>();
                    if (tempCustomInteractible != null && tempCustomInteractible.isInteractible && tempCustomInteractible.grabType == GrabType.Pinch)
                    {
                        if (Vector3.Distance(tempCustomInteractible.transform.position,hit.point) < tempCloseDistance)
                        {
                            tempCloseDistance = Vector3.Distance(tempCustomInteractible.transform.position, PinchPoint());
                            SelectedPinchInteractible = tempCustomInteractible;
                            SelectionLine.SetPosition(0, transform.position);
                            SelectionLine.SetPosition(1, SelectedPinchInteractible.transform.position);
                        }
                    }
                }
            }
        }
    }

    public void GeneratePoints()
    {
        points = new Vector3[numPoints];
 
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        
        // needed for sphere projection
        //float angleIncrement = Mathf.PI * 2 * goldenRatio;
        for (int i = 0; i < numPoints; i++)
        {
            float dst = Mathf.Pow(i / (numPoints - 1f),pow);
            float angle = 2 * Mathf.PI * goldenRatio * i;

            float x = dst * Mathf.Cos(angle);
            float y = dst * Mathf.Sin(angle);
            float z = coneHeight;

            
            points[i] = new Vector3(x,y,z);
            points[i] = RotatePointsAroundPivot(points[i], transform.position, coneRotation);

            ///
            /// This Code below can generate points and then project them onto a sphere.
            /// good for creating enemy vision cones or collidion avoidance. 
            /// learned from this link belod: 
            /// https://www.youtube.com/watch?v=bqtqltqcQhw
            ///
            /* 
            float t = (float)i / numPoints;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            points[i] = new Vector3(x, y, z);
            //Instantiate(prefab, points[i], transform.rotation);
            */

        }
    }
    public Vector3 RotatePointsAroundPivot(Vector3 point, Vector3 pivot,Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }


}
