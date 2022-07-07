using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CopyTransform))]
public class CopyTransBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CopyTransform myScript = (CopyTransform)target;
        if (GUILayout.Button("Copy Transform"))
        {
            myScript.transformThis();
        }
    }
}