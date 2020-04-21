using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // public components

    // private components 
    Rigidbody myRigidbody;


    // public variables 
    public float lifetime;
    public float speed;

    // private variables 
    float count;
    Vector3 direction;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
       
        count = 0;
       // transform.rotation = Quaternion.Euler(direction);
        myRigidbody.velocity = speed * -transform.forward;
        // set direction to new direction
        // set count to zero

    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > lifetime)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetDirection(Vector3 dir)
    {
        transform.rotation = Quaternion.Euler(dir);
    }
    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.collider.tag;
        switch (colTag)
        {
            case "Player":
                gameObject.SetActive(false);
                // hit enemy
                break;
            case "Obstacle":
                gameObject.SetActive(false);
                // destroy the obstacle? 
                break;
            default:
                gameObject.SetActive(false);
                // has no tag, or no recognized tag
                break;
        }
    }

}
