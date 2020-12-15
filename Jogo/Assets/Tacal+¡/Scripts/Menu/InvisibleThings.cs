using UnityEngine;
using System.Collections;

public class InvisibleThings : MonoBehaviour {

	void Awake () {
		GameObject cadastropanel;
		GameObject abapanelprof;

		cadastropanel = GameObject.Find ("CadastroPanel");
		abapanelprof = GameObject.Find ("AbaPanelProfessor");

		abapanelprof.SetActive (false);
		cadastropanel.SetActive (false);


	}

}
