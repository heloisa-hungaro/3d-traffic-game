using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pontuacao : MonoBehaviour {

	public Text scoreText;

	void Start () {
		scoreText.GetComponent<Text> ();

	
	}
	
	void Update () {
		if (Tempo.timerIsActive) {
			scoreText.text = "Pontuação:       " + DragHandler.score;
		}

	}
}
