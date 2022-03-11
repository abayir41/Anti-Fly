using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour {
	bool MouseDrag2 = false;
//	public Text Deneme,Deneme2;
//	public Vector2 Konum;
    void OnMouseDown(){
		
//		Vector2 Clickk = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		MouseDrag2 = true;

	}
     void OnMouseUp(){
//		Vector2 Dragg = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		MouseDrag2 = false;
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//		foreach (Touch touch in Input.touches) {
//		 Konum = new Vector2 (touch.position.x, touch.position.y);
//		}
		Vector2 Konum = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//		Deneme.text = (int)Input.GetTouch(0).position.x + "X";
//		Deneme2.text = (int)Input.GetTouch(0).position.y + "Y";
//		Vector2 Konum =  new Vector2(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y);
		if (MouseDrag2) {
			transform.position = Konum;
		}
	}

}
