﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private float deltaTime = 0;

    public Text fpsCounter;
    public Text clock;

    public MouseManager mouseManager;

    public GameObject buildTools;
    public bool buildModeOn = false;

    public GameObject pauseMenu;
    public bool gameIsPaused = false;

    public Weather weather;

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

        if (weather.currentTime >= 0 && weather.currentTime < 1200)
        {
			clock.text = "Day: " + weather.day + ", " + string.Format("{0}:{1}", weather.hours, weather.minutes + " AM");
        }
        else
        {
			clock.text = "Day: " + weather.day + ", " + string.Format("{0}:{1}", weather.hours, weather.minutes + " PM");
        }
    }
}
