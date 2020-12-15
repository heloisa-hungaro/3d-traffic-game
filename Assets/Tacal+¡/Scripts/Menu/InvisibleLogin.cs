using UnityEngine;
using System.Collections;

public class InvisibleLogin : MonoBehaviour {


	void Awake () {
		GameObject login;
		login = GameObject.Find ("LoginPanel");
		login.SetActive (false);

		GameObject avan;
		avan = GameObject.Find ("Avancar");
		avan.SetActive (false);

		GameObject error;
		error = GameObject.Find ("UsuarioSenhaError");
		error.SetActive (false);
	}

}
