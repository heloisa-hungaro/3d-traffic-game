using UnityEngine;
using System.Collections;

public class InvisibleLogin : MonoBehaviour {


	void Awake () {
		GameObject login;
		login = GameObject.Find ("LoginPanel");
		login.SetActive (false);
	}

}
