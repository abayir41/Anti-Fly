using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject Yukleme,Ayarlar,Menug;
	public float Yuklemesayi,Giris,Timer,SpawnDelay;
	public static float Reklam;
	public float FlySayif,EffectSoundf,SinekSesif,adchance,Reklamdebug;
	public Slider YuklemeSlider,FlySayilari,EffectSound,SinekSesi;
	public Text Sineklik, Sprey, Zehir,SinekSayi,EffectSati,SinekSesiSayi;
	public bool Reklambool;
	// Use this for initialization
	void Start () {
		Reklam = PlayerPrefs.GetFloat ("Ad");
		Giris = PlayerPrefs.GetFloat("Giris");
		if (Giris > 0) {
			FlySayif = PlayerPrefs.GetFloat ("SinekSayi");
			EffectSoundf = PlayerPrefs.GetFloat ("EffectSes");
			SinekSesif = PlayerPrefs.GetFloat ("SinekSesi");
			FlySayilari.GetComponent<Slider> ().value = FlySayif;
			SinekSesi.GetComponent<Slider> ().value = SinekSesif;
			EffectSound.GetComponent<Slider> ().value = EffectSoundf;
		}
		Yukleme.SetActive (false);
		Menug.SetActive (true);
		Yuklemesayi = 0;
		Sineklik.text = PlayerPrefs.GetFloat ("Sineklik").ToString ();
		Zehir.text = PlayerPrefs.GetFloat ("Zehir").ToString ();
		Sprey.text = PlayerPrefs.GetFloat ("Spray").ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Yuklemesayi > 0){
			Yukleme.SetActive (true);
			Menug.SetActive (false);
		}
		Reklam += Time.deltaTime;
		if (Reklam > 40) {
			adchance = Random.Range (1, 4);
			Reklambool = true;
		}
		if (adchance > 1 && Reklambool) {
			Reklam = 0;
			Reklambool = false;
		}
		if (adchance < 2 && Reklambool) {
			Reklam = 0;
			Reklambool = false;
		}
		YuklemeSlider.GetComponent<Slider> ().value = Yuklemesayi;
		PlayerPrefs.SetFloat ("Reklam", Reklam);
		PlayerPrefs.SetFloat ("SinekSayi", FlySayif);
		PlayerPrefs.SetFloat ("SinekSesi", SinekSesif);
		PlayerPrefs.SetFloat ("EffectSes", EffectSoundf);
		PlayerPrefs.SetFloat ("Giris", Giris);
		FlySayif = FlySayilari.GetComponent<Slider> ().value;
		SinekSesif = SinekSesi.GetComponent<Slider> ().value;
		EffectSoundf = EffectSound.GetComponent<Slider> ().value;
		SinekSayi.text = FlySayilari.GetComponent<Slider> ().value.ToString();
		EffectSati.text = EffectSound.GetComponent<Slider> ().value.ToString ();
		SinekSesiSayi.text = SinekSesi.GetComponent<Slider> ().value.ToString ();
		Reklamdebug = Reklam;
	}
	public void Load_Level(int Level_Index){
		StartCoroutine (Load_Progress (Level_Index));

	}
	IEnumerator Load_Progress(int Level_Index){
		AsyncOperation Opreration = SceneManager.LoadSceneAsync (Level_Index);
		while (!Opreration.isDone) {
			Yuklemesayi = Mathf.Clamp01 (Opreration.progress / 0.9f);
			yield return null;
		}
	}
	public void Exit(){
		Application.Quit ();
	}
	public void AyarlarAc(){
		Giris = 1;
		Ayarlar.SetActive (true);
		Menug.SetActive (false);
	}
	public void MenuAc(){
		Menug.SetActive (true);
		Ayarlar.SetActive (false);
	}
}
