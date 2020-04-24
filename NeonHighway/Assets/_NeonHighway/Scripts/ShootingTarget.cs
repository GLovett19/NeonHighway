using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingTarget : EnemyStateManager
{
    // when hit rotate down, wait for a period of time, stand back up; 
    //public components
    public GameObject rotationPoint;

    //public Camera cam;
    //private components 
    

    //public fields
    public float DeathTimer = 3; // how long this target should stay down after being hit 
    public float count = 0; // general counter
    public float speed = 1; // how quickly this target falls down

    // private fields 


    public override void Update()
    {
        ChangeEnemyState();
        if (enemyState != EnemyState.Dying)
        {
            ChangeEnemyAction();
            Stand();
        }
    }
    public override void  Die()
    {
        // roatate to x = 90
        if (rotationPoint.transform.localEulerAngles.x < 90)
        {
            count += Time.deltaTime * speed;
            // Debug.Log(rotationPoint.transform.localEulerAngles.x);
            rotationPoint.transform.localRotation = Quaternion.Lerp(rotationPoint.transform.localRotation,
                Quaternion.Euler(90, 0, 0), count);
            if (count >= 1)
            {
                count = 0;
            }

        }
        else
        {
            if (count < DeathTimer)
            {
                count += Time.deltaTime;
            }
            else
            {
                Health += 1;
                SetEnemyState(EnemyState.Idle);
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
