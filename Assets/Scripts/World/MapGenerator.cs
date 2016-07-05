using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    
	private static MapGenerator _instance;

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
    public PlanetPreset planet;

    private Tile[,] map;
    private int worldWidth;
    private int worldHeight;

	void Awake()
	{
        if (planet == null)
        {
            planet = PlanetPreset.CreateInstance<PlanetPreset>();
            planet.planetTileInfo = new PlanetPreset.Tiles[1];
            planet.planetTileInfo[0].tile = Tile.TileType.Grass;
            planet.planetTileInfo[0].layercount = 100;
            planet.planetTileInfo[0].buildable = false;
            planet.planetTileInfo[0].traversable = false;

            planet.planetResourceInfo = new PlanetPreset.Resources[1];
            planet.planetResourceInfo[0].resource = Tile.TileResource.None;
            planet.planetResourceInfo[0].layercount = 100;
        }

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

                    tile.x = (int)tile.transform.position.x;
                    tile.y = (int)tile.transform.position.y;


                    Vector2 position = worldZoom * (new Vector2(x, y));
                    float noise = Mathf.PerlinNoise(position.x + worldSeed, position.y + worldSeed);
                    noise *= 100;

                    while ( noise > 0.0f )
                    {
                        for( int index = 0; index < planet.planetTileInfo.Length; index++)
                        {
                            noise -= planet.planetTileInfo[index].layercount;
                            if( noise <= 0.0f)
                            {
                                tile.type = planet.planetTileInfo[index].tile;
                                tile.buildable = planet.planetTileInfo[index].buildable;
                                tile.traversable = planet.planetTileInfo[index].traversable;
                                break;
                            }
                        }
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

        noise *= 100;

        while (noise > 0.0f)
        {
            for (int index = 0; index < planet.planetResourceInfo.Length; index++)
            {
                noise -= planet.planetResourceInfo[index].layercount;
                if (noise <= 0.0f)
                {
                    tile.resources = planet.planetResourceInfo[index].resource;
                    break;
                }
            }
        }
    }
}
