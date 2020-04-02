using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingTarget : MonoBehaviour
{
    // when hit rotate down, wait for a period of time, stand back up; 
    //public components
    public Collider targetHitbox;
    public GameObject rotationPoint;

    public PopupText popText;
    public GameObject canvas;
    //public Camera cam;
    //private components 
    public ScoreKeeper scoreKeeper;

    //public fields
    public int Health = 1;
    public float DeathTimer = 3;
    public float count = 0;
    public float speed = 1;

    // private fields 

    public void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // there should only ever be one score keeper in the scene so this is fine 
    }

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
            scoreKeeper.AddPlayerScore(100,transform);
            CreatePopupText("100");
        }
    }

    public void Die()
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

    public void CreatePopupText(string text)
    {
        // old code does not rely on object pooler 
       // PopupText instance = Instantiate(popText);       
       // instance.transform.SetParent(canvas.transform, false);
       // instance.myText.text = text;

        GameObject popupText =ObjectPoolingManager.Instance.GetObject("PopupText");
        popupText.SetActive(true);
        popupText.transform.SetParent(canvas.transform, false);
        popupText.transform.localPosition = Vector3.zero;
        popupText.GetComponent<PopupText>().count = 0;
        popupText.GetComponent<PopupText>().myText.text = text;
        popupText.GetComponent<PopupText>().verticalMotionScale = 100;
        popupText.GetComponent<PopupText>().horizontalMotionScale = 50;
        popupText.GetComponent<PopupText>().verticalMotionSpeed = 1;
        popupText.GetComponent<PopupText>().horizontalMotionSpeed = 5;
        
    }
}
