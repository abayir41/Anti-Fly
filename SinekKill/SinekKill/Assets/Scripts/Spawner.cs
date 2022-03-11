using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] Fly;
	public Transform[] Positions;
	public float SpawnDelay,Timer;
	// Use this for initialization
	void Start () {
		SpawnDelay = 0.5f;
		Timer = SpawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (FlyControl.FlyCounts < PlayerPrefs.GetFloat("SinekSayi")) {
			Timer -= Time.deltaTime;
		}
		if (Timer < 0) {
			Instantiate (Fly [Random.Range (0, Fly.Length)], Positions[Random.Range (0, Positions.Length)].position, Quaternion.identity);
			Timer = SpawnDelay;
		}
	}
}
