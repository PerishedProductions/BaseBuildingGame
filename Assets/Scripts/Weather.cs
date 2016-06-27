using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Weather : MonoBehaviour {

    private int dayLength;
    private int dayStart;
    private int nightStart;
    public int hours;
    public int minutes;

    public float currentTime;
    private float cycleSpeed;
    public bool isDay;
    public Light sun;
    public GameObject earth;

    // Use this for initialization
    void Start () {
        dayLength = 1440;
        dayStart = 360;
        nightStart = 540;
        currentTime = 360;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 0.1f * Time.deltaTime * 5;
        //starts the day
        if (currentTime >= dayStart && currentTime < nightStart)
        {
            isDay = true;
            sun.intensity += 0.001f;
        }
        //starts the night
        if (currentTime >= nightStart && currentTime < dayLength)
        {
            isDay = false;
            sun.intensity -= 0.001f;
        }
        //resets the day
        if (currentTime >= dayLength)
        {
            currentTime = 0;
        }
        //makes the intensity not go under 0.02f
        if(sun.intensity < 0.2f)
        {
            sun.intensity = 0.2f;
        }
        //makes the intensity not go over 1f
        if (sun.intensity > 1)
        {
            sun.intensity = 1;
        }

        hours = Mathf.FloorToInt(currentTime / 60);
        minutes = (int)currentTime % 60;
    }
}
