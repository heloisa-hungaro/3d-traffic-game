using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PontuacaoFinal : MonoBehaviour {

	public Text scorefinalText;


	// Use this for initialization
	void Start () {
		scorefinalText.GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
			scorefinalText.text = " "+ DragHandler.score;
	}
}
