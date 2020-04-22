using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public BezierSpline spline;
    //duration based movement
    public bool fixedDuration;
    public float duration;
    public bool goingForeward = true;

    // velocity based motion
    public float velocity;

    public bool lookForward;
    public SplineWalkerMode mode;
    


    public float progress;



    public virtual void Update()
    {
        if (fixedDuration)
        {
            if (goingForeward)
            {
                progress += Time.deltaTime / (duration);
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
                progress -= Time.deltaTime / (duration);
                if (progress <= 0f)
                {
                    progress = -progress;
                    goingForeward = true;
                }
            }
        }
        else
        {
            if (goingForeward)
            {

                if (progress >= 1f)
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
                        progress = 1f;
                        goingForeward = false;
                    }
                }
                else
                {
                    float pathOnePercent = Vector3.Distance(spline.GetPoint(progress), spline.GetPoint(progress + 0.1f));
                    
                    float realOnePercent = spline.splineLength * 0.1f;                   
                    float Distortion = realOnePercent / pathOnePercent;
                    float realPercentToMove = (velocity * Time.deltaTime) / spline.splineLength;
                    progress = (realPercentToMove * Distortion) + progress;
                }
            }
            else
            {
                if (progress <= 0f)
                {
                    progress = 0;
                    goingForeward = true;
                }
                else
                {
                    float pathOnePercent = Vector3.Distance(spline.GetPoint(progress), spline.GetPoint(progress - 0.1f));

                    float realOnePercent = spline.splineLength * 0.1f;
                    float Distortion = realOnePercent / pathOnePercent;
                    float realPercentToMove = (-velocity * Time.deltaTime) / spline.splineLength;
                    progress = (realPercentToMove * Distortion) + progress;
                }
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
