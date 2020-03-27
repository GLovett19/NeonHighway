﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpllineWalker : MonoBehaviour
{
    public BezierSpline spline;
    public float duration;

    public bool lookForward;
    public SplineWalkerMode mode;
    public bool goingForeward = true;


    public float progress;
    public virtual void Update()
    {
        if (goingForeward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForeward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress <= 0f)
            {
                progress = -progress;
                goingForeward = true;
            }
        }
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}
