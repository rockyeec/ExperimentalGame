using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraManager))]
public class Editor_CameraManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Apply Changes"))
        {
            CameraManager cameraManager = target as CameraManager;
            cameraManager.ApplyChanges();
        }
    }
}
