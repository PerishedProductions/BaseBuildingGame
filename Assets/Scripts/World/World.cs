using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public GameObject emptyTilePrefab;
    private int offset = 1;

    public int worldHeight;
    public int worldWidth;
    public float worldHorizontalShift;
    public float worldVerticalShift;
    public float worldZoom;
    public int worldSeed;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        Vector2 shift = new Vector2(worldHorizontalShift, worldVerticalShift);
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                GameObject tile = (GameObject)Instantiate(emptyTilePrefab, new Vector3(x * offset, y * offset, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

                tile.transform.parent = this.transform;
                tile.name = tile.transform.position.x + ", " + tile.transform.position.y;

                tile.GetComponent<Tile>().x = (int)tile.transform.position.x;
                tile.GetComponent<Tile>().y = (int)tile.transform.position.y;


                Vector2 position = worldZoom * (new Vector2(x, y)) + shift;
                float noise = Mathf.PerlinNoise(position.x + worldSeed, position.y + worldSeed);

                if (0.2 > noise)
                {
                    tile.GetComponent<Tile>().type = Tile.TileType.Water;
                }

                if (noise >= 0.2 && 0.3 > noise)
                {
                    tile.GetComponent<Tile>().type = Tile.TileType.Sand;
                    tile.GetComponent<Tile>().buildable = true;
                }

                if (noise >= 0.3 && 0.5 > noise)
                {
                    tile.GetComponent<Tile>().type = Tile.TileType.Grass;
                    tile.GetComponent<Tile>().buildable = false;
                }

                if (noise >= 0.5 && 0.7 > noise)
                {
                    tile.GetComponent<Tile>().type = Tile.TileType.Dirt;
                    tile.GetComponent<Tile>().buildable = true;
                }

                if (noise >= 0.7)
                {
                    tile.GetComponent<Tile>().type = Tile.TileType.Mountain;
                    tile.GetComponent<Tile>().buildable = false;
                }
                GenerateFeatures(tile, x, y);
            }
        }
    }

    private void GenerateFeatures(GameObject tile, int X_Position, int Y_Position)
    { 
        float noise = Mathf.PerlinNoise(X_Position, Y_Position);

        if (0.15 > noise)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.Oil;
        }

        if (noise >= 0.15 && 0.40 > noise)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.None;
        }

        if (noise >= 0.4 && 0.6 > noise)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.Wood;
        }

        if (noise >= 0.6 && 0.7 > noise)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.Crystal;
        }

        if (noise >= 0.7 && 0.8 > noise)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.Gas;
        }

        if (noise >= 0.9)
        {
            tile.GetComponent<Tile>().feature = Tile.TileFeature.Ore;
        }
    }


    public Tile GetTileAt(int x, int y)
    {
        Transform tile = transform.FindChild(x + ", " + y);
        if (tile != null)
        {
            return tile.gameObject.GetComponent<Tile>();
        }
        return null;
    }

}
