using UnityEditor;
using Rock.Utilities;
using UnityEngine;

[CustomEditor(typeof(IconMaker))]
public class Editor_IconMaker : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        IconMaker iconMaker = target as IconMaker;
        
        if (GUILayout.Button("Create Icon"))
        {
            iconMaker.CreateIcon();
        }
    }
}
