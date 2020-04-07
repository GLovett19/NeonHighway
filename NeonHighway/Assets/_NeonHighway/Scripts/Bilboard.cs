using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Bilboard : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<SteamVR_PlayArea>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2*transform.position-target.position);
        
    }
}
