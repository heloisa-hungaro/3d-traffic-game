using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour {



	void Awake () {
		GameObject vol;
		vol = GameObject.Find ("Slider_volume");
		vol.SetActive (false);
	}

}
