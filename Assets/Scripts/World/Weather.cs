using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Weather : MonoBehaviour
{

    //All of them variables
    public WeatherPreset preset;

    private int dayLength;
    private int dayStart;
    private int nightStart;
    public int day = 1;
    public int hours;
    public int minutes;

    private float cycleSpeed;
    public float currentTime;
    public bool isDay;
    public bool isRaining;
    public Light sun;
    public ParticleSystem Rain;

    // Use this for initialization
    void Start()
    {
        //If there is a preset defined use its values
        if (preset != null)
        {
            dayLength = preset.dayLength * 60;
            dayStart = preset.dayStart * 60;
            nightStart = preset.nightStart * 60;
            currentTime = 360;
        }
        else
        {
            dayLength = 1440;
            dayStart = 360;
            nightStart = 540;
            currentTime = 360;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += 0.1f * Time.deltaTime * 5;

        //Checks if it's raining
        if (Rain.isPlaying == true)
        {
            isRaining = true;
        }
        else
        {
            isRaining = false;
        }

        //Cheks if it's raining during the day
        if (isRaining == true && currentTime >= dayStart && currentTime < nightStart)
        {
            sun.intensity -= 0.001f;
            //Makes the sun intensity not go under 0.5
            if (sun.intensity < 0.5f)
            {
                sun.intensity = 0.5f;
            }
            isDay = true;
        }

        //Checks if it's day
        if (isRaining == false && currentTime >= dayStart && currentTime < nightStart)
        {
            isDay = true;
            //makes the sun intensity increase if it's day
            sun.intensity += 0.001f;
        }

        //Cheks if it's raining during the night
        if (isRaining == true && currentTime >= nightStart && currentTime < dayLength || isRaining == true && currentTime < dayStart)
        {
            isDay = false;
            //Decreases the sun intensity if it's night
            sun.intensity -= 0.001f;
            if (isRaining == true && currentTime >= dayStart - 10)
            {
                sun.intensity += 0.002f;
            }
            if (sun.intensity > 0.5f)
            {
                sun.intensity = 0.5f;
            }
        }

        //Checks if it's night
        if (isRaining == false && currentTime >= nightStart && currentTime < dayLength || isRaining == false && currentTime < dayStart)
        {
            isDay = false;
            //Decreases the sun intensity if it's night
            sun.intensity -= 0.001f;
        }

        //Makes the sun intensity not go under 0.2f
        if (sun.intensity < 0.2f)
        {
            sun.intensity = 0.2f;
        }

        //Makes the intensity not go over 1f
        if (sun.intensity > 1)
        {
            sun.intensity = 1;
        }

        //Resets the day
        if (currentTime >= dayLength)
        {
            currentTime = 0;
        }

        //Add one day whenever the time is at 0
        if (currentTime == 0)
        {
            day++;
        }

        //Converts the time into hours and minutes
        hours = Mathf.FloorToInt(currentTime / 60);
        minutes = (int)currentTime % 60;

        //Makes a random chance for the rain to start
        if (UnityEngine.Random.Range(0, 100000) <= 5)
        {
            Rain.Play();
        }
        //Cheks if it's raining
        if (isRaining)
        {
            //Makes a random chance for the rain to stop
            if (UnityEngine.Random.Range(0, 100000) <= 5)
            {
                Rain.Stop();
            }
        }
    }
}