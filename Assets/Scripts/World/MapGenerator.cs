using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    
	public static MapGenerator _instance;

	public static MapGenerator Instance 
	{
		get
		{
			if (_instance == null) 
			{
				GameObject go = new GameObject ("MapGenerator");
				go.AddComponent<MapGenerator> ();
			}
			return _instance;
		}
	}

	private int offset = 1;

    private int worldSeed;
    private float worldZoom;
    public GameObject emptyTilePrefab;

    private Tile[,] map;
    private int worldWidth;
    private int worldHeight;

	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad (this);
	}

    public void InitializeMap( int Width, int Height, float Zoom)
    {
        map = new Tile[Width, Height];

        for( int x = 0; x < Width; x++)
        {
            for( int y = 0; y < Height; y++)
            {
                GameObject temp = (GameObject)Instantiate(emptyTilePrefab, new Vector3(x * offset, y * offset, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                map[x, y] = temp.GetComponent<Tile>();
            }
        }

        worldZoom = Zoom;
        worldWidth = Width;
        worldHeight = Height;
    }

    public Tile[,] GenerateMap(int Seed)
    {
        worldSeed = Seed;

        if ( map != null)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    Tile tile = map[x, y];
                    tile.transform.parent = this.transform;
                    tile.name = tile.transform.position.x + ", " + tile.transform.position.y;

                    tile.GetComponent<Tile>().x = (int)tile.transform.position.x;
                    tile.GetComponent<Tile>().y = (int)tile.transform.position.y;


                    Vector2 position = worldZoom * (new Vector2(x, y));
                    float noise = Mathf.PerlinNoise(position.x + worldSeed, position.y + worldSeed);

                    if (0.2 > noise)
                    {
                        tile.GetComponent<Tile>().type = Tile.TileType.Water;
                        tile.GetComponent<Tile>().buildable = false;
                    }

                    if (noise >= 0.2 && 0.3 > noise)
                    {
                        tile.GetComponent<Tile>().type = Tile.TileType.Sand;
                        tile.GetComponent<Tile>().buildable = true;
                    }

                    if (noise >= 0.3 && 0.5 > noise)
                    {
                        tile.GetComponent<Tile>().type = Tile.TileType.Grass;
                        tile.GetComponent<Tile>().buildable = true;
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
                    GenerateFeatures(tile, position.x + worldSeed, position.y + worldSeed);
                }
            }
        }      

        return map;
    }

    private void GenerateFeatures(Tile tile, float X_Position, float Y_Position)
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
}
