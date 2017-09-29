using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;
using System;
using System.Linq;
using Horsey;

[CustomEditor(typeof(JSONDataSource))]
public class JSONDataSourceEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        JSONDataSource source = (JSONDataSource)this.target;
        source.URL = EditorGUILayout.TextField("Source URL", source.URL);
        Dictionary<string, Type> types = source.FindAllSQLTypes();
        Type sourceType = source.HorseyDataType;
        int selected = 0;
        int z = 0;
        foreach(KeyValuePair<string, Type> kvp in types)
        {
            if (sourceType == kvp.Value)
            {
                selected = z;
            }
            z++;
        }
        string[] keys = source.FindAllSQLTypes().Keys.ToArray();

        selected = EditorGUILayout.Popup("Types:", selected, keys);
        z = 0;
        foreach (KeyValuePair<string, Type> kvp in types)
        {
            if (z == selected)
            {
                source.HorseyDataType = kvp.Value;
            }
            z++;
        }

        if (GUILayout.Button("Populate"))
        {
            Task.Run(new System.Action(source.PopulateData));
        }
        EditorGUILayout.LabelField("Results: ", source.JSONResults);
    }

}