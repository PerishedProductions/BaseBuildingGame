using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Weather : MonoBehaviour {

	//All of them variables
	public WeatherPreset preset;

	private int dayLength;
    private int dayStart;
    private int nightStart;
	public int day = 1;
    public int hours;
    public int minutes;

    public float currentTime;
    private float cycleSpeed;
    public bool isDay;
    public Light sun;
    public ParticleSystem Rain;

    // Use this for initialization
    void Start () {
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

		//Add one day whenever the time is at 0
		if (currentTime == 0) {
			day++;
		}

		//Converts the time into hours and minutes
        hours = Mathf.FloorToInt(currentTime / 60);
        minutes = (int)currentTime % 60;

        if(UnityEngine.Random.Range(0, 100) <= 100)
        {
            Rain.Play();
        }
    }
}
