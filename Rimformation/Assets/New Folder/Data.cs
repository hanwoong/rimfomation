using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[CustomEditor(typeof(Data))]
public class ListTesterInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Vectors"), true);
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Time"), true);
        serializedObject.ApplyModifiedProperties();
    }
}

public class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3[] Vectors;
    public float[] Time;
    

    void Start()
    {
        transform.position = Vectors[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
