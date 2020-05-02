using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    // movement vriables 

    public BezierSpline MySpline;
    public float velocity;
    public bool lookForeward;

    // attack variables 
    public float BulletSpeedCompensation = 5f;
    public bool isAiming; // should I be Aiming right now? 
    public float aimSpeed; // aim speed
    public float rateOfFire; // how quickly this enemy can shoot
    public int ammo; // how many attacks this enemy can make before reloading
    public float reloadSpeed;// how quickly this enemy can realod after attacking


    GameObject currEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if (currEnemy != null)
        {
            Destroy(currEnemy);
        }
        currEnemy = Instantiate(EnemyToSpawn, transform.position, transform.rotation);
        currEnemy.GetComponent<EnemyStateManager>().velocity = velocity;
        currEnemy.GetComponent<EnemyStateManager>().enemyState = EnemyStateManager.EnemyState.Idle;
        if (MySpline && currEnemy.GetComponent<SplineWalker>())
        {
            currEnemy.GetComponent<SplineWalker>().spline = MySpline;           
            //currEnemy.GetComponent<SplineWalker>().velocity = velocity;
            currEnemy.GetComponent<SplineWalker>().lookForward = lookForeward;
        }
        if (currEnemy.GetComponentInChildren<EnemyAttack>())
        {
            currEnemy.GetComponentInChildren<EnemyAttack>().BulletSpeedCompensation = BulletSpeedCompensation;
            currEnemy.GetComponentInChildren<EnemyAttack>().isAiming = isAiming; // should I be Aiming right now? 
            currEnemy.GetComponentInChildren<EnemyAttack>().aimSpeed = aimSpeed; // aim speed
            currEnemy.GetComponentInChildren<EnemyAttack>().rateOfFire = rateOfFire; // how quickly this enemy can shoot
            currEnemy.GetComponentInChildren<EnemyAttack>().ammo = ammo; // how many attacks this enemy can make before reloading
            currEnemy.GetComponentInChildren<EnemyAttack>().reloadSpeed = reloadSpeed;// how quickly this enemy can realod after attacking
}
    }
}
