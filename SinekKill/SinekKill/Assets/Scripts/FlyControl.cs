using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyControl : MonoBehaviour {
	public float Speed,Enter,Timet,Timer,TimerRandom,Rotation,RotateRandom,Olu;
	public float Rotate,RotateTime,FlyChange,FlyWalk,Speed2,FlyVisibleTimer,Sayi,Saydamlas,ZehirTimer;
	public static int FlyCounts;
	public bool Enter2,Flipx,TimerEnable,TimerEnable2,FlyWalkBool,FlyWalking,FlyUcmaOlu,FlyYerdeOlu,TimerEnable3,Saydamlasma,FlySprayOlu;
	public bool ZehirEnable;
	public static bool Zehir;
	public SpriteRenderer Fly;
	// Use this for initialization
	void Start () {
		if (transform.localPosition.x < 0) {
			Speed = 3f;
			Speed2 = 3f;
			Flipx = false;
		} else {
			Speed = -3f;
			Speed2 = -3f;
			Flipx = true;
		}
		Enter2 = true;
		TimerEnable = false;
		Timer = 0.49f;
		TimerRandom = Random.Range (0.5f, 0.9f);
		TimerEnable2 = true;
		TimerEnable3 = true;
		FlyChange = 30;
		FlyWalking = false;
		FlyCounts = FlyCounts + 1;
		Olu = 1;
		Saydamlas = 1;
		Zehir = false;
	}
	
	// Update is called once per frame
	void Update () {
		Sayi = FlyCounts;
		GetComponent<Animator> ().SetBool ("SinekDur", FlyWalking);
		GetComponent<Animator> ().SetBool ("YerdeOlu", FlyYerdeOlu);
		GetComponent<Animator> ().SetBool ("UcmaOlu", FlyUcmaOlu);
		GetComponent<Animator> ().SetBool ("SprayOlu", FlySprayOlu);
		GetComponent<SpriteRenderer> ().flipX = Flipx;
		Rotation = transform.localRotation.z * 180; 
		transform.Translate (Speed * Time.deltaTime, 0, 0);
		if (Rotation < 0 && Olu > 0) {
			GetComponent<SpriteRenderer> ().flipY = true;
		} 
		if (Rotation > 0 && Olu > 0){
			GetComponent<SpriteRenderer> ().flipY = false;
	    }
		if (Speed > 0) {
			Flipx = false;
		}
		if (Speed < 0) {
			Flipx = true;
		}
		if (Flipx && !FlyYerdeOlu) {
			Speed2 = -3;
		} 
		if (!Flipx && !FlyYerdeOlu){
			Speed2 = 3;
		}
		if (Enter > 0 && Enter2) {
			Timet += Time.deltaTime;
		}
		if (Timet > 0.65f && Olu > 0) {
			GetComponent<CapsuleCollider2D> ().enabled = true;
			TimerEnable = true;
		}	
		if (Timet > 2f && Enter2) {
			Enter2 = false;
		}
		if (TimerEnable && TimerEnable2 && TimerEnable3){
			Timer += Time.deltaTime;
		}
		if (Timer > TimerRandom){
			FlyChange = Random.Range (1, 25);
			TimerRandom = Random.Range (0.5f, 0.9f);
			Timer = 0;
		}
		if (FlyWalkBool) {
			FlyWalk += Time.deltaTime;
		}
		if (FlyChange < 2){
			FlyWalking = true;
			Speed = 0;
			TimerEnable2 = false;
			FlyWalkBool = true;
		}
		if (FlyWalk > 2f) {
			transform.Translate (Speed2 * Time.deltaTime * 20, 0, 0);
			transform.Rotate (0,0,10 * Olu);
			FlyWalkBool = false;
			FlyWalk = 0;
			TimerEnable2 = true;
			FlyChange = 30;
		}
		if (FlyChange > 1 && FlyChange < 26) {
			FlyWalking = false;
			Speed = 3;
			RotateRandom = Random.Range (-180,180);
			RotateTime = Random.Range (0.2f,0.3f);
			FlyChange = 30;
		}
		if (RotateTime > 0) {
			RotateTime -= Time.deltaTime;
		}
		if (RotateRandom < 0 && RotateTime > 0){
			Rotate = -180 - RotateRandom;
			transform.Rotate (0, 0, Rotate * Time.deltaTime * 2);
		}
		if (RotateRandom > 0 && RotateTime > 0){
			Rotate = 180 + RotateRandom;
			transform.Rotate (0, 0, Rotate * Time.deltaTime * 2);
		}
		if (!GetComponent<SpriteRenderer> ().isVisible) {
			FlyVisibleTimer += Time.deltaTime;
		}
		if (FlyVisibleTimer > 4f && Olu > 0) {
			Destroy (gameObject);
			FlyCounts = FlyCounts - 1;
		}
		if (FlyVisibleTimer > 4f && Olu < 1) {
			Destroy (gameObject);
		}
		if (Saydamlasma) {
			Saydamlas -= Time.deltaTime * 0.2f;
			Fly.color = new Color (Fly.color.r,Fly.color.g,Fly.color.b,Saydamlas);
		}
		if (Saydamlas < 0.02f) {
			Destroy (gameObject);
		}
		if (Zehir && Olu > 0){
			TimerEnable3 = false;
			FlySprayOlu = true;
			ZehirTimer += Time.deltaTime;
		}
		if (ZehirTimer > 1 ) {
			Speed = 5;
			transform.Rotate (0, 0, 600 * Time.deltaTime);
		}
		if (ZehirTimer > 3) {
			GetComponent<Rigidbody2D> ().freezeRotation = false;
			GetComponent<Rigidbody2D> ().gravityScale = 1;
			Speed = 0;
			Speed2 = 0;
			Olu = 0;
			ZehirTimer = 0;
			FlyCounts = FlyCounts - 1;
			ButtonControl.ZehirKill = ButtonControl.ZehirKill + 1;
			ButtonControl.FlyKill = ButtonControl.FlyKill + 1;
			ButtonControl.ParaKazandirma = ButtonControl.ParaKazandirma + 1;
		}
	}
		
	void OnCollisionEnter2D (Collision2D col){
		//Olme 
		if (col.gameObject.tag == "Olum" && Olu > 0){
			TimerEnable3 = false;
			GetComponent<CapsuleCollider2D> ().enabled = false;
			Speed = 0;
			Saydamlasma = true;
			Olu = 0;
			Speed2 = 0;
			FlyCounts = FlyCounts - 1;
			ButtonControl.SineklikKill = ButtonControl.SineklikKill + 1;
			ButtonControl.ParaKazandirma = ButtonControl.ParaKazandirma + 1;
			ButtonControl.FlyKill = ButtonControl.FlyKill + 1;
			if (FlyWalkBool) {
				FlyYerdeOlu = true;
			} else {
				FlyUcmaOlu = true;
			}
		}
		//SprayOlum
		if (col.gameObject.tag == "Sprey" && Olu > 0) {
			FlySprayOlu = true;
			TimerEnable3 = false;
			Speed = 0;
			Olu = 0;
			Speed2 = 0;
			FlyCounts = FlyCounts -1;
			ButtonControl.SprayKill = ButtonControl.SprayKill + 1;
			ButtonControl.ParaKazandirma = ButtonControl.ParaKazandirma + 1;
			ButtonControl.FlyKill = ButtonControl.FlyKill + 1;
			GetComponent<Rigidbody2D> ().freezeRotation = false;
			GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
		// Wall 1
		if (col.gameObject.tag == "Wall" && Timet > 0.5f && Olu > 0) {
			Speed = Speed * -1;
			Speed2 = Speed2 * -1;
			if (Flipx) {
				Flipx = false;
			}else {Flipx = true;}
		}
		if (col.gameObject.tag == "Wall" && Enter < 1) {
			Enter = Enter + 1;
			GetComponent<CapsuleCollider2D> ().enabled = false;
			Enter2 = true;
		}
		//Wall 2
		if (col.gameObject.tag == "Wall2" && Timet > 0.5f && Olu > 0) {
			Speed = Speed * -1;
			Speed2 = Speed2 * -1;
			if (Flipx) {
				Flipx = false;
			}else {Flipx = true;}
		}
		if (col.gameObject.tag == "Wall2" && Enter < 1) {
			Enter = Enter + 1;
			GetComponent<CapsuleCollider2D> ().enabled = false;
			Enter2 = true;
		}
		//Wall 3
		if (col.gameObject.tag == "Wall3" && Timet > 0.5f && Olu > 0) {
			Speed = Speed * -1;
			Speed2 = Speed2 * -1;
			if (Flipx) {
				Flipx = false;
			}else {Flipx = true;}
		}
		if (col.gameObject.tag == "Wall3" && Enter < 1) {
			Enter = Enter + 1;
			GetComponent<CapsuleCollider2D> ().enabled = false;
			Enter2 = true;
		}
		// Wall 4
		if (col.gameObject.tag == "Wall4" && Timet > 0.5f && Olu > 0) {
			Speed = Speed * -1;
			Speed2 = Speed2 * -1;
			if (Flipx) {
				Flipx = false;
			}else {Flipx = true;}
		}
		if (col.gameObject.tag == "Wall4" && Enter < 1) {
			Enter = Enter + 1;
			GetComponent<CapsuleCollider2D> ().enabled = false;
			Enter2 = true;
		}
	}
}
