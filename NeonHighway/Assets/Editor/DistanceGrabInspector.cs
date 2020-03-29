using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DistanceGrab))]



public class DistanceGrabInspector : Editor
{
    private DistanceGrab distGrab;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private int selectedIndex = -1;

    bool showAllValues;
    /*
    Tool lastTool = Tool.None;

    private void OnEnable()
    {
        lastTool = Tools.current;
        Tools.current = Tool.None;
    }
    private void OnDisable()
    {
        Tools.current = lastTool;
    }
    */

    private void OnSceneGUI()
    {
        //Tools.current = Tool.None;
        distGrab = target as DistanceGrab;
        handleTransform = distGrab.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;


        Vector3[] uiPoints = new Vector3[distGrab.points.Length];

       
        for (int i = 0; i < distGrab.points.Length; i++)
        {
            uiPoints[i] = ShowPoint(i);
        }
    }
    public override void OnInspectorGUI()
    {
        showAllValues = EditorGUILayout.Toggle(showAllValues);
        if (showAllValues)
        {
            DrawDefaultInspector();
        }
        try
        {
            distGrab.numPoints = (int)EditorGUILayout.Slider(distGrab.numPoints, 0, 300);

            distGrab.coneHeight = EditorGUILayout.Slider(distGrab.coneHeight, 1, 10);
        }
        catch
        {
            Debug.Log("Distance grabber not yet assigned");
        }
        //DrawDefaultInspector();
        if (selectedIndex >= 0 && selectedIndex < distGrab.points.Length)
        {
            DrawSelectedPointInspector();
        }
        distGrab = target as DistanceGrab;
        if (GUILayout.Button("GeneratePoints"))
        {
            Undo.RecordObject(distGrab, "Generate Points");
            distGrab.GeneratePoints();
            EditorUtility.SetDirty(distGrab);
        }
    }


    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(distGrab.points[index]);
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = Color.white;
        if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
        {
            selectedIndex = index;
            Repaint();
        }
        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(distGrab, "Move Point");
                EditorUtility.SetDirty(distGrab);
                distGrab.points[index] = handleTransform.InverseTransformPoint(point);
            }
        }
        return point;
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", distGrab.points[selectedIndex]);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(distGrab, "Move Point");
            EditorUtility.SetDirty(distGrab);
            distGrab.points[selectedIndex] = handleTransform.InverseTransformPoint(point);
        }
    }
}
