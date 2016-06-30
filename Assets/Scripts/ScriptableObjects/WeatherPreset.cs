using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Wheather Preset", menuName = "Weather Preset", order = 1)]
public class WeatherPreset : ScriptableObject {

	public string planetType;

	public int dayLength = 24;
	public int dayStart = 4;
	public int nightStart = 22;
    public int gameStartTime = 4;
}
