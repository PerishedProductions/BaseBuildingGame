using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(World))]
public class WorldGenEditor : Editor
{

     public override void OnInspectorGUI()
    {
        World world = (World)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate new map"))
        {
            world.worldZoom = Random.Range(1.9f, 99.9f);
            world.GenerateMap();
        }
    }
	
}
