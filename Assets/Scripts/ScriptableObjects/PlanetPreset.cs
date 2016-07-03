using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Planet Preset", menuName = "Planet Preset", order = 2)]
public class PlanetPreset : ScriptableObject {

    [Serializable]
    public struct Tiles
    {
        public Tile.TileType tile;
        public float layercount;
        public bool buildable;
        public bool traversable;
    }

    [Serializable]
    public struct Resources
    {
        public Tile.TileFeature resource;
        public float layercount;
    }

    public Tiles[] planetTileInfo;
    public Resources[] planetResourceInfo;


}
