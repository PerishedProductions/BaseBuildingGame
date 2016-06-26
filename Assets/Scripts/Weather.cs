using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Weather : MonoBehaviour {

    private int dayLength;
    private int dayStart;
    private int nightStart;
    public float currentTime;
    private float cycleSpeed;
    private bool isDay;
    public Light sun;
    public GameObject earth;


    // Use this for initialization
    void Start () {
        dayLength = 2400;
        dayStart = 0;
        nightStart = 1200;
        currentTime = 0;
        //StartCoroutine(TimeOfDay());
        earth = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 0.1f * Time.deltaTime * 100;
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
    }
/*
  private IEnumerator TimeOfDay()
    {
        while (true)
        {
            currentTime += 1;
            int hours = Mathf.RoundToInt(currentTime / 60);
            int minutes = (int)currentTime % 60;
            Debug.Log(hours + ":" + minutes);
            yield return new WaitForSeconds(1F / cycleSpeed);
        }
    }
    */
}
