using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour {


	public void Start () {
		GameObject vol;
		vol = GameObject.Find ("slider_volume");
		vol.SetActive (false);
	}

}
