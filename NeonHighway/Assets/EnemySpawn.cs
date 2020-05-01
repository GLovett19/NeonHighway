using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spawn Enemies ");
            foreach (SpawnPoint sp in spawnPoints)
            {
                sp.Spawn();
            }
        }
        
    }
}
