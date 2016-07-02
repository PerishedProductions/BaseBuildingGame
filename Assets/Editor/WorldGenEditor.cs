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
            world.worldSeed = Random.Range(1, 999999);
            world.RegenerateMap();
        }
    }
	
}
