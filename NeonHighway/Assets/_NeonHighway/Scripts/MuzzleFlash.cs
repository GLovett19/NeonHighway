using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    //Particles
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void muzzleFlashEffect()
    {
        // I can't test the location of the muzzle flash 
        //Instantiate(muzzleFlash, transform.position, Quaternion.identity);
    }
}
