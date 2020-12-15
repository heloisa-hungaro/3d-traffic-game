using UnityEngine;
using System.Collections;

public class VisibleLogin : MonoBehaviour {

	public void VisibleClick(GameObject login)
	{
		if (login.activeSelf) {
			login.SetActive (false);
		} 

		else {
			login.SetActive (true);
		}
	}
}
