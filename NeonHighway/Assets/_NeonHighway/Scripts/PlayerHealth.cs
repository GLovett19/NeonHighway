using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// this script handles the players health and taking damage, when a bullet collides with the player or when an obstacle collides 
    /// when the player dies what needs to happen? 
    /// 1a) Stop movement along spline 
    /// 1b> stop movement side to side 
    /// 2) Open Lose Menu
    /// 3) disable pickups
    /// 
    /// </summary>
    public int health;
    public SplineVehicleMovement mySplineMover;
    public SplineWalker mySplineWalker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            if (mySplineWalker != null)
            {
                mySplineWalker.velocity = 0;
            }
            Debug.Log("Player dead");
        }
    }

    public void TakeDamage(int val)
    {
        health -= val;
    }
}
