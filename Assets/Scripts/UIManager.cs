using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public MouseManager mouseManager;

    public void Booldoze()
    {
        mouseManager.StartBuild(Tile.TileType.Empty);
    }

    public void BuildFloor()
    {
        mouseManager.StartBuild(Tile.TileType.Floor);
    }

    public void BuildWall()
    {
        mouseManager.StartBuild(Tile.TileType.Wall);
    }
	
}
