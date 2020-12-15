using UnityEngine;
using System.Collections;

public class InvisibleGeneric : MonoBehaviour {

	public void Invisible(GameObject objeto)
	{
		objeto.SetActive (false);
	}
}
