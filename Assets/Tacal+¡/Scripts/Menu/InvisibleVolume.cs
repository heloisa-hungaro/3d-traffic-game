using UnityEngine;
using System.Collections;

public class InvisibleVolume : MonoBehaviour {

	void Awake () {
		GameObject vol;
		vol = GameObject.Find ("Slider_volume");
		vol.SetActive (false);
	}

}
