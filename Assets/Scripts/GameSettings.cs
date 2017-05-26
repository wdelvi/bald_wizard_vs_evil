using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSettings : MonoBehaviour 
{
	public Player player;
	public Text mainText;
	public Slider progressSlider;
	public GameObject building;
	public GameObject bug;
	public float gameLength;
	public float stunLength;
	public float firstSpawnAddittionalDelay;
	public float spawnRateStart;
	public float spawnRateFloor;
	public float spawnX;
	public float minSpawnY;
	public float maxSpawnY;
}
