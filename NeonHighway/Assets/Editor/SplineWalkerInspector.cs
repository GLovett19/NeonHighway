using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineWalker))]
public class SplineWalkerInspector : Editor
{
    private SplineWalker walker;
    public override void OnInspectorGUI()
    {
        walker = target as SplineWalker;
        // display all normal stuff first 

        // assigned spline
        EditorGUI.BeginChangeCheck();
        BezierSpline spline = (BezierSpline)EditorGUILayout.ObjectField(walker.spline,typeof(BezierSpline),true);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(walker, "Spline");
            EditorUtility.SetDirty(walker);
            walker.spline = spline;
        }
      
        //looking foreward
        EditorGUI.BeginChangeCheck();
        bool lookForeward = EditorGUILayout.Toggle("lookForeward", walker.lookForward);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(walker, "Toggle lookForeward");
            EditorUtility.SetDirty(walker);
            walker.lookForward = lookForeward;
        }
        // fixed duration
        EditorGUI.BeginChangeCheck();
        bool fixedDuration = EditorGUILayout.Toggle("FixedDuration", walker.fixedDuration);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(walker, "Toggle fixedDuration");
            EditorUtility.SetDirty(walker);
            walker.fixedDuration = fixedDuration;
        }

        // does this walker have a fixed duration
        if (walker.fixedDuration)
        {
            //movement mode
            EditorGUI.BeginChangeCheck();
            SplineWalkerMode mode = (SplineWalkerMode)EditorGUILayout.EnumFlagsField("Mode", walker.mode);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(walker, "Mode");
                EditorUtility.SetDirty(walker);
                walker.mode = mode;
            }
            // show the duration
            EditorGUI.BeginChangeCheck();
            float duration = EditorGUILayout.FloatField("Duration", walker.duration);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(walker, "Duration");
                EditorUtility.SetDirty(walker);
                walker.duration = duration;
            }
            //show the going foreward bool
            EditorGUI.BeginChangeCheck();
            bool goingForeward = EditorGUILayout.Toggle("goingForeward", walker.goingForeward);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(walker, "Toggle goingForeward");
                EditorUtility.SetDirty(walker);
                walker.goingForeward = goingForeward;
            }
            
        }
        else
        {
            //show the velocity field
            EditorGUI.BeginChangeCheck();
            float velocity = EditorGUILayout.FloatField("velocity", walker.velocity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(walker, "velocity");
                EditorUtility.SetDirty(walker);
                walker.velocity = velocity;
            }
        }
        // base.OnInspectorGUI();

    }

}
