using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetThings : MonoBehaviour {

	// Use this for initialization
	public void Reseta () {
		DragHandler.score = 0;
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
		//Application.LoadLevel (Application.loadedLevelName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
