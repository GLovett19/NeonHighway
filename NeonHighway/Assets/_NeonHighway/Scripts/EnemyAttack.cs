using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{


    //rotate towards target at a set velocity if the target is in line of sight.
    // if the player is near enough the aim direction, tell the enemy "Aim is Good".
    // have a seperate method for actual shooting. 


        // public components
    public Transform target; // thing to shoot at

        // private components
   private LineRenderer myLineRenderer; // indication of aim, obscures vision so I removed it for now

    // public variables 
    public float BulletSpeedCompensation = 5f;
    public bool isAiming; // should I be Aiming right now? 
    public float aimSpeed; // aim speed
    public float rateOfFire; // how quickly this enemy can shoot
    public int ammo; // how many attacks this enemy can make before reloading
    public float reloadSpeed;// how quickly this enemy can realod after attacking
    public LayerMask lm; // layers to ignore

    //private variables 
    
    bool AimisGood; // 
    bool isAttacking;
    float count;
    float ammoCount;

    void Start()
    {
        SetTarget();
        myLineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (count > 0)
        {
            count -= Time.deltaTime;
        }
        if (isAiming)
        {
            UpdateAim();
            /*
            if (AimisGood && count <= 0 && !isAttacking)
            {
                isAttacking = false;
                Attack();
            }
            */
        }
    }

    public void UpdateAim()
    {
        float step = aimSpeed * Time.deltaTime;
        // roatate to look at 

        //transform.LookAt(target);
        SplineWalker myWalker = target.GetComponentInParent<SplineWalker>();
                                        // target velocity           *      Time to target  = distance 
        Vector3 movementPredition = (myWalker.velocity * myWalker.spline.GetDirection(myWalker.progress)) * (Vector3.Distance(target.position,transform.position)/BulletSpeedCompensation);

        Quaternion temp = Quaternion.LookRotation(transform.position - (target.position+(movementPredition)));// get the current speed of the vehicle AND the current speed of your head 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, temp,step);

        /*
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.forward, out hit, 100, lm);
        myLineRenderer.SetPosition(0, transform.position);
        
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            AimisGood = true;
            myLineRenderer.SetPosition(1, hit.point);
            Debug.Log("AimIsGood");
        }
        else
        {
            AimisGood = false;
            myLineRenderer.SetPosition(1, -transform.forward * 100);
        }
        */
        AimisGood = true;

    }

    public void Attack()
    {
        // do the attack things 
        // might be a good idea to have a "windup" where the texture changes and we can have an audio cue for an attacking enemey 

        // instantiate bullet prefab
        GameObject EnemyBullet = ObjectPoolingManager.Instance.GetObject("EnemyBullet");
        EnemyBullet.transform.position = transform.position;
        EnemyBullet.transform.rotation = transform.rotation;
        EnemyBullet.SetActive(true);
        
        //EnemyBullet.GetComponent<EnemyBullet>().SetDirection(transform.rotation.eulerAngles);
        
        if (ammoCount > 1)
        {
            ammoCount -= 1;
            count = rateOfFire;
        }
        else
        {
            ammoCount = ammo;
            count = reloadSpeed;
        }

        GetComponentInParent<EnemyStateManager>().SetEnemyState(EnemyStateManager.EnemyState.Idle);
    }
    public void SetTarget()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
    }
    public void SetAiming(bool bval)
    {
        isAiming = bval;
    }

    public bool GetAimIsGood()
    {
        return AimisGood;
    }
    public bool GetRoFisGood()
    {
        return count <= 0;
    }
    public bool GetIsAttacking()
    {
        Debug.Log("is attacking : " + isAttacking);
        return isAttacking;
        
    }
}
