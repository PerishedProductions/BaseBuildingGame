using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum TileType { Empty, Sand, Water, Grass, Dirt, Mountain, Floor, Wall }
    public enum TileFeature { None, Pavement, Gas, Oil, Crystal, Ore, Wood }
    public TileType type = TileType.Empty;
    public TileFeature feature = TileFeature.None;
    public bool buildable;

    public Sprite empty;
    public Sprite floor;
    public Sprite wall;

    public int x;
    public int y;

    private SpriteRenderer sprRenderer;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (type)
        {
            case TileType.Empty:
                sprRenderer.sprite = empty;
                break;
            case TileType.Floor:
                sprRenderer.sprite = floor;
                break;
            case TileType.Wall:
                sprRenderer.sprite = wall;
                break;
        }
    }

}
