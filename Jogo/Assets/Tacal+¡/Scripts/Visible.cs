using UnityEngine;
using System.Collections;

public class Visible : MonoBehaviour {

	GameObject vol;

	public void Start()
	{
		vol = GameObject.Find ("slider_volume");
	}

	public void VisibleClick()
	{
		if (vol.activeSelf) {
			vol.SetActive (false);
		} 

		else {
			vol.SetActive (true);
		}
	}

}
