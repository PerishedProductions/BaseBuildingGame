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
        dayLength = 1440;
        dayStart = 300;
        nightStart = 1200;
        currentTime = 720;
        StartCoroutine(TimeOfDay());
        earth = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 0.1f;
        if (currentTime > 0 && currentTime < dayStart)
        {
            isDay = false;
            sun.intensity -= 0.001f;
        }
        else if (currentTime >= dayStart && currentTime < nightStart)
        {
            isDay = true;
            sun.intensity += 0.001f;
        }
        else if (currentTime >= nightStart && currentTime < dayLength)
        {
            isDay = false;
            sun.intensity -= 0.001f;
        }
        else if (currentTime >= dayLength)
        {
            currentTime = 0;
        }
        if(sun.intensity < 0.2f)
        {
            sun.intensity = 0.2f;
        }
        if (sun.intensity > 1)
        {
            sun.intensity = 1;
        }
        float currentTimeF = currentTime;
        float dayLengthF = dayLength;
        earth.transform.eulerAngles = new Vector3(0, 0, (-(currentTimeF / dayLengthF) * 360) + 90);
    }

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
}
