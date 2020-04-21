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
    private LineRenderer myLineRenderer; // indication of aim, in general

        // public variables 
    public float speed; // aim speed
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
        myLineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        count -= Time.deltaTime;
        UpdateAim();
        if (AimisGood && count <= 0 && !isAttacking)
        {
            isAttacking = false;
            Attack();
        }
    }

    public void UpdateAim()
    {
        float step = speed * Time.deltaTime;
        // roatate to look at 
        Quaternion temp = Quaternion.LookRotation(transform.position - target.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, temp,step);

        RaycastHit hit;
        Physics.SphereCast(transform.position, 0.3f, -transform.forward, out hit, 1000, lm);
        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, hit.point);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            AimisGood = true;
        }
        else
        {
            AimisGood = false;
        }

    }

    public void Attack()
    {
        // do the attack things 
        // instantiate bullet prefab
        GameObject EnemyBullet = ObjectPoolingManager.Instance.GetObject("EnemyBullet");
        EnemyBullet.transform.position = transform.position;
        EnemyBullet.transform.rotation = transform.rotation;
        EnemyBullet.SetActive(true);
        
        //EnemyBullet.GetComponent<EnemyBullet>().SetDirection(transform.rotation.eulerAngles);
        
        // set rate of fire counter 
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
       

        // decrement ammo
    }
    public void SetTarget(Transform var)
    {
        target = var;
    }
}
