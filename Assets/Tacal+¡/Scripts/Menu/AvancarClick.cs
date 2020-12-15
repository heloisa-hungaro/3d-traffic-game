using UnityEngine;
using System.Collections;

public class AvancarClick : MonoBehaviour {

	private static int count = 0;
	public GameObject tut1;
	public GameObject tut2;
	public GameObject tut3;

	public void ButtonClick () {
		count++;

		if (count == 1) {
			tut1.SetActive (false);
			tut2.SetActive (true);
		} if (count == 2) {
			tut2.SetActive (false);
			tut3.SetActive (true);
		} if (count == 3) {
			tut3.SetActive (false);
			this.gameObject.SetActive (false);
			count = 0;
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
