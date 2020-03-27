using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingTarget : MonoBehaviour
{
    // when hit rotate down, wait for a period of time, stand back up; 

    public Collider targetHitbox;
    public GameObject rotationPoint;

    public int Health = 1;
    public float DeathTimer = 3;
    public float count = 0;
    public float speed = 1;

    public void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Stand();
        }
    }

    public void Damage(int val)
    {
        if (Health > 0)
        {
            //Debug.Log("Damage taken" + 1);
            Health--;
        }
    }

    public void Die()
    {
        // roatate to x = 90
        if (rotationPoint.transform.localEulerAngles.x < 90)
        {
            count += Time.deltaTime * speed;
            Debug.Log(rotationPoint.transform.localEulerAngles.x);
            rotationPoint.transform.localRotation = Quaternion.Lerp(rotationPoint.transform.localRotation,
                Quaternion.Euler(90, 0, 0), count);
            if (count >= 1)
            {
                count = 0;
            }
           
        }
        else
        {
            count += Time.deltaTime;
            if (count >= DeathTimer)
            {
                Health += 1;
                count = 0;
            }
        }
    }

    public void Stand()
    {
        if (rotationPoint.transform.localRotation.x > 0)
        {
            rotationPoint.transform.localRotation = Quaternion.Lerp(rotationPoint.transform.localRotation,
                Quaternion.Euler(rotationPoint.transform.InverseTransformDirection(0, 0, 0)), .03f);
        }
    }
}
