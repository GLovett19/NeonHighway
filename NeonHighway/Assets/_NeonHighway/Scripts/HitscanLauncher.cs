using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanLauncher : Launcher
{
    
    public override void Spawn()
    {
        //Debug.Log("Opject Spawn Called");
        if (spawnObject)
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                // damage target or create decal or whatever. 
                Debug.Log(hit.transform.name);
                try
                {
                    hit.transform.GetComponentInParent<ShootingTarget>().Damage(1);
                }

                catch
                {
                    //Debug.Log("Hit Target cannot recieve Damage");
                }
            }
            GameObject BulletTrail = ObjectPoolingManager.Instance.GetObject("bullet");
            BulletTrail.SetActive(true);
            BulletTrail.GetComponent<MuzzleEffect>().SetPositions(transform.position, hit.point);

            // old code functions without an object pooler in the scene
            /*
            GameObject InstanseObject = Instantiate(spawnObject, transform.position, transform.rotation) as GameObject;
            if (InstanseObject.GetComponent<MuzzleEffect>())
            {
                InstanseObject.GetComponent<MuzzleEffect>().SetPositions(transform.position,hit.point);
            }
            */
        }
    }
}
