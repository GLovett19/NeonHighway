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
    private SplineWalker splineWalker;
    private EnemyAttack enemyAttack;

    //public fields
    public EnemyState enemyState;
    public EnemyAction enemyAction;
    public int Health = 1;
    public float velocity;

    // private fields 
    float count;

    public void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // there should only ever be one score keeper in the scene so this is fine 
        splineWalker = GetComponent<SplineWalker>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        
    }
    public void Start()
    {
        SetEnemyState(EnemyState.Idle);
    }
    public virtual void Update()
    {
        ChangeEnemyState();
        if (enemyState != EnemyState.Dying)
        {
            ChangeEnemyAction();
        }
    }

    public void SetEnemyState(EnemyState newState)
    {
        if (newState != enemyState)
        {
            switch (newState)
            {
                case EnemyState.Idle:
                    {
                        splineWalker.velocity = 0f;
                    }
                    break;
                case EnemyState.Moving:
                    {
                        splineWalker.velocity = velocity;
                    }
                    break;
                case EnemyState.Dying:
                    {
                        splineWalker.velocity = 0f;
                        count = 2;
                        // start die animation?
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
        enemyState = newState;
    }
    public void ChangeEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                {
                    if (splineWalker.spline && 
                        (splineWalker.goingForeward && splineWalker.progress<1) || (!splineWalker.goingForeward && splineWalker.progress>0))
                    {
                        SetEnemyState(EnemyState.Moving);
                    }
                    if (Health <= 0)
                    {
                        SetEnemyState(EnemyState.Dying);
                    }
                }
                break;
            case EnemyState.Moving:
                {
                    if (!splineWalker.spline|| 
                        (splineWalker.goingForeward && splineWalker.progress >= 1) || (!splineWalker.goingForeward && splineWalker.progress <= 0))
                    {
                        SetEnemyState(EnemyState.Idle);
                    }
                    if (Health <= 0)
                    {
                        SetEnemyState(EnemyState.Dying);
                    }
                }
                break;
            case EnemyState.Dying:
                {
                    Die();
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
                        //Debug.Log("Attack");
                        enemyAttack.Attack();
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
        enemyAction = newAction;
    }
    public void ChangeEnemyAction()
    {
        switch (enemyAction)
        {
            case EnemyAction.None:
                {
                    if (enemyAttack != null && enemyAttack.GetAimIsGood() && enemyAttack.GetRoFisGood() && !enemyAttack.GetIsAttacking())
                    {
                        SetEnemyAction(EnemyAction.Attack);
                    }
                }
                break;
            case EnemyAction.Attack:
                {
                    SetEnemyAction(EnemyAction.None);
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

    public virtual void Die()
    {
        if (count > 0)
        {
            count -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
       
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
