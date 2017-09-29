using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VirtualUIAreaChart))]
public class VirtualUIAreaChartEditor : Editor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        VirtualUIAreaChart chart = (VirtualUIAreaChart)target;
        EditorGUILayout.LabelField("Virtual UI Chart");
        EditorGUILayout.Separator();
        if (GUILayout.Button("Populate in Design Mode"))
        {
            chart.SetDirtyEditor();
        }
        EditorGUILayout.Separator();
        base.OnInspectorGUI();
    }
}
