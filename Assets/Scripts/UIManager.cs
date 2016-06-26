using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private float deltaTime = 0;

    public Text fpsCounter;
    public MouseManager mouseManager;

    public GameObject buildTools;
    public bool buildModeOn = false;

    public GameObject pauseMenu;
    public bool gameIsPaused = false;

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

    public void ToggleOnBuildMode()
    {
        buildTools.SetActive(!buildModeOn);
        buildModeOn = !buildModeOn;
    }

    public void TogglePaused()
    {
        pauseMenu.SetActive(!gameIsPaused);
        gameIsPaused = !gameIsPaused;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        fpsCounter.text = string.Format("{1:0.} FPS", msec, fps);
    }
}
