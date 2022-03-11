using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supurge : MonoBehaviour {
	public GameObject Wall,SupurgeButon;
	// Use this for initialization
	void Start () {
		Wall = ButtonControl.Walls;
		SupurgeButon = ButtonControl.SupurgeButons;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-Time.deltaTime, 0, 0);
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Destroyer") {
			Wall.SetActive (true);
			SupurgeButon.SetActive (true);
			Destroy (gameObject);
		}
	}
}
