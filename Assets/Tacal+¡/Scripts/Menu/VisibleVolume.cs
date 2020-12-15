using UnityEngine;
using System.Collections;

public class VisibleVolume : MonoBehaviour {

	public void VisibleClick(GameObject vol)
	{
		if (vol.activeSelf) {
			vol.SetActive (false);
		} 

		else {
			vol.SetActive (true);
		}
	}
}
