using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplineVehicleMovement : MonoBehaviour
{
    //public components
    public SteeringWheel sw;
    public PedalTrigger pedal;
    public SplineWalker walker;

    //Temporary Components 
    public Text DebugText;

    //public fields 
    public float maxAngle = 30;
    public float handling = 1f;
    public float RoadLimit = 5f;
    public float BumpDistance = 1f;

    public float maxSpeed = 99;
    public float Acceleration;
    float Speed = 1;
   // public Vector3 position;

    public void Update()
    {
        // the input angle from the steering wheel;
        float angle = maxAngle * (sw.angle / 360);


        // debut text for testing vehicle movement 
        DebugText.text = ("angle = " + angle.ToString() + "\n"
            + "Speed = " + Speed + "\n"
            + "Road Position Local" + transform.localPosition.ToString() + "\n");


        // Left right movement handled here 
        if (Mathf.Abs(transform.localPosition.x) < RoadLimit)
        {
            transform.localPosition += new Vector3(angle * handling * Time.deltaTime, 0, 0);
        }
        else if (transform.localPosition.x > 0)
        {
            transform.localPosition += new Vector3(BumpDistance * Time.deltaTime * -1, 0, 0);
        }
        else if (transform.localPosition.x < 0)
        {
            transform.localPosition += new Vector3(BumpDistance * Time.deltaTime, 0, 0);
        }

        //the speed of the vehicle foreward and backward 
        if (Speed <= maxSpeed && Speed >= 1)
        {
            Speed += Acceleration * pedal.c_Axis * Time.deltaTime;
        }
        else
        {
            Speed = Mathf.Clamp(Speed, 1, maxSpeed);
        }
        walker.velocity = Speed;

        


        // when the vehicle reaches the end of its track 
        if (walker.progress >= 1)
        {
            FindObjectOfType<AppManager>().scorePasser = FindObjectOfType<ScoreKeeper>().GetPlayerScore();
            FindObjectOfType<MenuGeneric>().SelectScene("LevelEndScene");
        }
    }

    // Impacts and Crashes
    public void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.collider.tag;
        switch (colTag)
        {
            case "Enemy":
                collision.collider.GetComponentInParent<ShootingTarget>().Damage(1);// replace shooting target with generic enemy parent script later 
                break;
            case "Obstacle":
                Crash(100);
                // slow down the vehicle and possibly cause damage
                break;
            default:
                break;
        }

    }

    public void Crash(int val)
    {
        if (Speed - val > 0)
        {
            Speed -= val;
        }
        else
        {
            Speed = 1;
        }
    }

}
