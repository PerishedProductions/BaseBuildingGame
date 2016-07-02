using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public GameObject emptyTilePrefab;

    public int worldHeight;
    public int worldWidth;
    public float worldZoom;
    public int worldSeed;

    private Tile[,] map;

    void Start()
    {
		MapGenerator.Instance.InitializeMap(worldWidth, worldHeight, worldZoom);
		map = MapGenerator.Instance.GenerateMap(worldSeed);
    }

   public void RegenerateMap()
    {
		map = MapGenerator.Instance.GenerateMap(worldSeed);
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
