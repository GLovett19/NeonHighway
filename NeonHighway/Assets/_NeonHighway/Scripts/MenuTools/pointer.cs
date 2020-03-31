using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pointer : MonoBehaviour
{
    public float defaultLength = 5.0f;
    public GameObject dot;
    public VRInputModule inputModule;

    private LineRenderer line = null;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default value for length or distance 
        PointerEventData data = inputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

        // raycast
        RaycastHit hit = CreateRaycast(targetLength);


        //default end 
        Vector3 endPosition = transform.position + (transform.forward * targetLength);


        // or based on hid
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }


        // set position of dot
        dot.transform.position = endPosition;


        //set position of line renderer
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);


        return hit;
    }
}
