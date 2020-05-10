using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeCounter : MonoBehaviour
{
    public float lifetime;
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= lifetime)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
