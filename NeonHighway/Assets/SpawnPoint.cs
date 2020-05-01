using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject EnemyToSpawn;
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
    }
}
