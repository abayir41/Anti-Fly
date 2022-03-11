using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {
	public GameObject SineklikNormal,SineklikVur,SineklikButon,Sineklik,Sprey,Zehir,SineklikAna,Spawnerg,Tik,Carpi,ZehirText;
	public GameObject SpreyButon,Spreylik,SprayEffect,Spreylik2,SupurgeButon,Wall,ZehirEffect,ResetButon,ZehirSatinal,SinekSes;
	public GameObject[] SupurgeObje;
	public static GameObject SupurgeButons, Walls;
	public static bool ZehirEnable2,SpreyEnable;
	public Transform []Position;
	public float Timer,SprayTimer,ZehirTimer,ParaStringer,FlyKillStringer,SinekSesi,EffectSesi,Reklam,adchange;
	public static float Money,FlyKill,ParaKazandirma,SineklikKill,ZehirKill,SprayKill;
	public bool TimerEnable,SprayBool,ZehirEnable,Reklambool;
	public Slider SpraySlider;
	public Text Para,FlyKillText;
	public AudioSource Audio;
	public AudioClip SineklikSes,SpreySes;
	// Use this for initialization
	void Start () {
		Reklam = PlayerPrefs.GetFloat ("Reklam");
		FlyKill = PlayerPrefs.GetFloat ("SinekOldurme");
		Money = PlayerPrefs.GetFloat ("Para");
		SupurgeButons = SupurgeButon;
		Walls = Wall;
		FlyControl.FlyCounts = 0;
		Audio = GetComponent<AudioSource> ();
		SineklikKill = PlayerPrefs.GetFloat ("Sineklik");
		ZehirKill = PlayerPrefs.GetFloat ("Zehir");
		SprayKill = PlayerPrefs.GetFloat ("Spray");
		Audio.volume = PlayerPrefs.GetFloat ("EffectSes") / 100f;
		SinekSesi =	PlayerPrefs.GetFloat ("SinekSesi");
		SinekSes.GetComponent<AudioSource> ().volume = SinekSesi / 100f; 
	}
	
	// Update is called once per frame
	void Update () {
		if (SprayTimer > 2f) {
			SprayTimer = 2f;
		}
		if (SprayTimer > 0 && SprayBool) {
			Spreylik2.GetComponent<BoxCollider2D> ().enabled = true;
			SprayTimer -= Time.deltaTime;
			SprayEffect.SetActive (true);
		}
		if (!SprayBool) {
			SprayTimer += Time.deltaTime * 3;
			Spreylik2.GetComponent<BoxCollider2D> ().enabled = false;
			SprayEffect.SetActive (false);
		}
		if (SprayTimer < 0) {
			Spreylik2.GetComponent<BoxCollider2D> ().enabled = false;
			SprayEffect.SetActive (false);
		}
		if (SprayTimer < -0.5f) {
			SprayTimer = -0.5f;
		}
		if (TimerEnable) {
			Timer += Time.deltaTime;
		}
		if (Timer > 0.1f) {
			SineklikVur.GetComponent<CircleCollider2D> ().enabled = false;
		}
		if (ZehirEnable) {
			ZehirTimer += Time.deltaTime;
		}
		if (ZehirTimer > 3){
			ZehirEffect.SetActive (false);
		}
		if (ZehirTimer > 4) {
			ZehirEnable = false;
			Spawnerg.SetActive (true);
			ZehirTimer = 0;
			FlyControl.Zehir = false;
		}
		if (FlyKill > 1000f) {
			FlyKillStringer = FlyKill / 1000f;
		} else {
			FlyKillStringer = FlyKill;
		}
		if (Money > 1000f) {
			ParaStringer = Money / 1000f;
		} else {
			ParaStringer = Money;
		}
		if (ParaKazandirma >= 5) {
			ParaKazandirma = ParaKazandirma - 5;
			Money = Money + 1;
		}
		Reklam += Time.deltaTime;
		if (Reklam > 60) {
			adchange = Random.Range (1, 4);
			Reklambool = true;
		}
		if (adchange > 1 && Reklambool) {
			Reklam = 0;
			Reklambool = false;
		}
		if (adchange < 2 && Reklambool) {
			Reklam = 0;
			Reklambool = false;
		}
		PlayerPrefs.SetFloat ("Para", Money);
		PlayerPrefs.SetFloat ("SinekOldurme", FlyKill);
		PlayerPrefs.SetFloat ("Spray", SprayKill);
		PlayerPrefs.SetFloat ("Sineklik", SineklikKill);
		PlayerPrefs.SetFloat ("Zehir", ZehirKill);
		PlayerPrefs.SetFloat ("Ad", Reklam);
		FlyKillText.text = FlyKillStringer.ToString ();
		Para.text = ParaStringer.ToString ();
		SpraySlider.GetComponent<Slider> ().value = SprayTimer;
	}
	//Sineklik
	public void SineklikBas(){
		Audio.PlayOneShot (SineklikSes, 1f);
		SineklikAna.GetComponent<PolygonCollider2D> ().enabled = false;
		SineklikVur.SetActive (true);
		SineklikVur.GetComponent<CircleCollider2D> ().enabled = true;
		SineklikNormal.SetActive (false);
		Timer = 0;
		TimerEnable = true;
	}
	public void SineklikBırak(){
		SineklikAna.GetComponent<PolygonCollider2D> ().enabled = true;
		SineklikVur.SetActive (false);
		SineklikNormal.SetActive (true);
	}
	public void SineklikAc(){
		ZehirSatinal.SetActive (false);
		Tik.SetActive (false);
		Carpi.SetActive (false);
		ZehirText.SetActive (false);
		ResetButon.SetActive (true);
		SpreyEnable = true;
		SineklikAna.SetActive (true);
		SineklikButon.SetActive (true);
		Sineklik.SetActive (false);
		Sprey.SetActive (false);
		Zehir.SetActive (false);
	}
	//Sprey
	public void SpreyAc(){
		ZehirSatinal.SetActive (false);
		Tik.SetActive (false);
		Carpi.SetActive (false);
		ZehirText.SetActive (false);
		ResetButon.SetActive (true);
		Spreylik.SetActive (true);
		SupurgeButon.SetActive (true);
		SpreyButon.SetActive (true);
		Zehir.SetActive (false);
		Sineklik.SetActive (false);
		Sprey.SetActive (false);
	}
	public void SprayAc(){
		SprayBool = true;
		Audio.PlayOneShot (SpreySes, 1f);
	}
	public void SprayBırak(){
		SprayBool = false;
	}
	public void Supurge(){
		Wall.SetActive (false);
		SupurgeButon.SetActive (false);
		Instantiate (SupurgeObje [Random.Range (0, SupurgeObje.Length)], Position[Random.Range (0, Position.Length)].position, Quaternion.identity);
	}
	//Reset
	public void Reset(){
		SupurgeButon.SetActive (false);
		SpreyEnable = false;
		ZehirEnable2 = false;
		Zehir.SetActive (true);
		Sineklik.SetActive (true);
		Sprey.SetActive (true);
		Spreylik.SetActive (false);
		SineklikAna.SetActive (false);
		SineklikButon.SetActive (false);
		SpreyButon.SetActive (false);
		ResetButon.SetActive (false);
	}
	public void ZehirSatin(){
		ZehirSatinal.SetActive (true);
		Tik.SetActive (true);
		Carpi.SetActive (true);
		ZehirText.SetActive (true);
	}
	public void Zehiralinmadi(){
		ZehirSatinal.SetActive (false);
		Tik.SetActive (false);
		Carpi.SetActive (false);
		ZehirText.SetActive (false);
	}
	public void Zehiralindi(){
		if (Money >= 50) {
			Audio.PlayOneShot (SpreySes, 1f);
			Money = Money - 50;
			SupurgeButon.SetActive (true);
			ZehirEnable2 = true;
			Sineklik.SetActive (false);
			Sprey.SetActive (false);
			ZehirEffect.SetActive (true);
			ZehirEnable = true;
			ResetButon.SetActive (true);
			Zehir.SetActive (false);
			Spawnerg.SetActive (false);
			FlyControl.Zehir = true;
			ZehirSatinal.SetActive (false);
			Tik.SetActive (false);
			Carpi.SetActive (false);
			ZehirText.SetActive (false);
		}
	}
	public void Menu(){
		SceneManager.LoadScene (0);
	}
	public void Vidrek(){
	}
	
}
