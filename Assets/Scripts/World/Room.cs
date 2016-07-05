using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

    public string Name;
    public string Description;
    public string[] Requirements;
    public bool BuildOutside;
    public bool BuildInside;

    public List<Tile> tiles = new List<Tile>();

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }

    public void DestroyRoom(Tile tile)
    {
        tile.room = null;
        tiles.Remove(tile);
    }

}
