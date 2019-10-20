using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateBody))]
public class GenerateBodyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GenerateBody script = (GenerateBody)target;
        if (GUILayout.Button("Generate Test Body"))
        {
            script.GenerateTestBody();
        }
    }
}
