using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Room Preset", menuName = "Room Preset", order = 3)]
public class RoomPreset : ScriptableObject
{
    public string Name;
    public string Description;
    public string[] Requirements;
    public bool BuildOutside;
    public bool BuildInside;


}
