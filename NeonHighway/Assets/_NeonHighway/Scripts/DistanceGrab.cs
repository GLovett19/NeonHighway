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
    //public CustomHand myHand;


    //Public fields 
    public int numPoints = 100;
    public float coneHeight = 10;
    [HideInInspector]
    public Vector3[] points;
    public LayerMask mask;

    public List<PhysicalObject> distanceGrabbable;
    public PhysicalObject distanceGrabbableSelected;

    //Private fields 
    float pow = 0.5f;

    private void Update()
    {
        // Raycast out for each point on the distance grabber;
        for (int i = 0; i < numPoints; i++)
        {
            //Debug.DrawLine(transform.position, transform.TransformPoint(points[i]), Color.green);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (transform.TransformPoint(points[i])- transform.position).normalized,out hit, 5f,mask,QueryTriggerInteraction.Ignore)) 
            {

                if (hit.collider.gameObject.GetComponentInParent<PhysicalObject>())
                {
                    Debug.Log("Cone Hit Object which can be grabbed");
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    // add the object to the list if it isnt already on it 
                    // check if it is has a point closer to the center 
                }
                else
                {
                    //Debug.DrawLine(transform.position, hit.point, Color.green);
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
            
            points[i] = new Vector3(x, y, coneHeight);


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
}
