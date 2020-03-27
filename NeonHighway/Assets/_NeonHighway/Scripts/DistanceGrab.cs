using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrab : MonoBehaviour
{
    [Range(0,300)]
    public int numPoints = 100;

    public Vector3[] points;
    public void GeneratePoints()
    {
        points = new Vector3[numPoints];


        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / numPoints;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            points[i] = new Vector3(x, y, z);
            //Instantiate(prefab, points[i], transform.rotation);
        }
    }
}
