using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private float deltaTime = 0;

    public Text fpsCounter;

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
	
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        fpsCounter.text = string.Format("{1:0.} FPS", msec, fps);
    }
}
