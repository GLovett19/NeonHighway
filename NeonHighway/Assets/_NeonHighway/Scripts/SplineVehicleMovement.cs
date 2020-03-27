using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplineVehicleMovement : MonoBehaviour
{
    //public components
    public SteeringWheel sw;

    //Temporary Components 
    public Text DebugText;

    //public fields 
    public float maxAngle = 30;
    public float handling = 1f;
    public float RoadLimit = 5f;
    public float BumpDistance = 1f;
   // public Vector3 position;

    public void Update()
    {
        float angle = maxAngle * (sw.angle / 360);

        DebugText.text = ("angle = " + angle.ToString() + "\n"
            //+ "Road Position = " + transform.position.ToString() + "\n"
            + "Road Position Local" + transform.localPosition.ToString() + "\n");

        if (Mathf.Abs(transform.localPosition.x) < RoadLimit)
        {
            //transform.position += transform.InverseTransformDirection(new Vector3(-angle * handling * Time.deltaTime, 0, 0));
            transform.localPosition += new Vector3(angle * handling * Time.deltaTime, 0, 0);
        }
        else if (transform.localPosition.x > 0)
        {
            //transform.position += transform.InverseTransformDirection(new Vector3(BumpDistance * Time.deltaTime, 0, 0));
            transform.localPosition += new Vector3(BumpDistance * Time.deltaTime * -1, 0, 0);
        }
        else if (transform.localPosition.x < 0)
        {
            //transform.position += transform.InverseTransformDirection(new Vector3(BumpDistance * Time.deltaTime * -1 , 0, 0));
            transform.localPosition += new Vector3(BumpDistance * Time.deltaTime, 0, 0);
        }


    }

}
