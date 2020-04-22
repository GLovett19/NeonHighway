using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    public enum EnemyState
    {
        None,
        Idle,
        Moving,
        Dying, 
    }
    public enum EnemyAction
    {
        None,
        Attack
    }

    //public components
    public Collider targetHitbox;

    public PopupText popText;
    public GameObject canvas;
    //public Camera cam;
    //private components 
    private ScoreKeeper scoreKeeper;

    //public fields
    public EnemyState enemyState;
    public EnemyAction enemyAction;
    public int Health = 1;
    public float Velocity = 1;

    // private fields 

    public void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // there should only ever be one score keeper in the scene so this is fine 
    }

    public void Update()
    {
    }

    public void SetEnemyState(EnemyState newState)
    {
        if (newState != enemyState)
        {
            switch (newState)
            {
                case EnemyState.Idle:
                    {
                    }
                    break;
                case EnemyState.Moving:
                    {
                    }
                    break;
                case EnemyState.Dying:
                    {
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
    }
    public void ChangeEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                {
                }
                break;
            case EnemyState.Moving:
                {
                }
                break;
            case EnemyState.Dying:
                {
                }
                break;
            default:
                {
                }
                break;
        }
    }


    public void SetEnemyAction(EnemyAction newAction)
    {
        if (newAction != enemyAction)
        {
            switch (enemyAction)
            {
                case EnemyAction.None:
                    {
                    }
                    break;
                case EnemyAction.Attack:
                    {
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
    }
    public void ChangeEnemyAction()
    {
        switch (enemyAction)
        {
            case EnemyAction.None:
                {
                }
                break;
            case EnemyAction.Attack:
                {
                }
                break;
            default:
                {
                }
                break;
        }
    }


    public void Damage(int val)
    {
        if (Health > 0)
        {
            //Debug.Log("Damage taken" + 1);
            Health--;
            scoreKeeper.AddPlayerScore(100, transform);
            CreatePopupText("100");
        }
    }

    public void Die()
    {

    }

    public void CreatePopupText(string text)
    {
        // old code does not rely on object pooler 
        // PopupText instance = Instantiate(popText);       
        // instance.transform.SetParent(canvas.transform, false);
        // instance.myText.text = text;

        GameObject popupText = ObjectPoolingManager.Instance.GetObject("PopupText");
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
