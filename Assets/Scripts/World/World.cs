using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public GameObject emptyTilePrefab;

    public int worldHeight;
    public int worldWidth;
    public float worldZoom;
    public int worldSeed;

    private Tile[,] map;
    public MapGenerator mapGenerator;

    void Start()
    {
        mapGenerator.InitializeMap(worldWidth, worldHeight, worldZoom);
        map = mapGenerator.GenerateMap(worldSeed);
    }

   public void RegenerateMap()
    {
        map = mapGenerator.GenerateMap(worldSeed);
    }


    public Tile GetTileAt(int x, int y)
    {
        if (x < worldWidth && x >= 0 && y < worldHeight && y >= 0)
        {
            Tile tile = map[x, y];
            if (tile != null)
            {
                return tile;
            }
        }

        return null;
    }

}
