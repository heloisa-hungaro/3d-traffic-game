using UnityEngine;
using System.Collections;

public class InvisibleThingsGame : MonoBehaviour {

	public GameObject fimPanel;

	public void Awake () {
		fimPanel.SetActive (false);
	}

}
