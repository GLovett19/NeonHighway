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
            Vector3 impact;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                // set the point of impact for the muzzle effect

                impact = hit.point;
                // damage target or create decal or whatever. 
                // there has to be a better way to do this I just don't know what it is
                //Debug.Log(hit.transform.name);
                try
                {
                    hit.transform.GetComponentInParent<ShootingTarget>().Damage(1);
                }

                catch
                {
                    //Debug.Log("Hit Target cannot recieve Damage");
                }
            }
            else
            {
                impact = transform.forward * 200;
            }
            GameObject BulletTrail = ObjectPoolingManager.Instance.GetObject("bullet");
            BulletTrail.SetActive(true);
            BulletTrail.GetComponent<MuzzleEffect>().SetPositions(transform.position, impact);

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
