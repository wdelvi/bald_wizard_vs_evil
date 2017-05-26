using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour 
{
	public Player player;
	public Text mainText;
	public Slider progressSlider;
	public GameObject building;
	public GameObject bugPrefab;
	public GameObject elevatePrefab;
	public float bossSpawnTimeRatio;
	public float gameLength;
	public float firstSpawnAddittionalDelay;
	public float spawnRateStart;
	public float spawnRateFloor;
	public float spawnX;
	public float bossSpawnX;
	public float minSpawnY;
	public float maxSpawnY;
	public float bossInitialForceMultiplier;
	public List<string> goodbyes;
}
