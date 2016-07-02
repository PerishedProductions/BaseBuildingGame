using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum TileType { Empty, Sand, Water, Grass, Dirt, Mountain, Floor, Wall }
    public enum TileFeature { None, Pavement, Gas, Oil, Crystal, Ore, Wood }
    public TileType type = TileType.Empty;
    public TileFeature feature = TileFeature.None;
    public bool buildable;

    public Sprite empty;
    public Sprite sand;
    public Sprite water;
    public Sprite grass;
    public Sprite dirt;
    public Sprite mountain;
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
            case TileType.Sand:
                sprRenderer.sprite = sand;
                break;
            case TileType.Water:
                sprRenderer.sprite = water;
                break;
            case TileType.Grass:
                sprRenderer.sprite = grass;
                break;
            case TileType.Dirt:
                sprRenderer.sprite = dirt;
                break;
            case TileType.Mountain:
                sprRenderer.sprite = mountain;
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
