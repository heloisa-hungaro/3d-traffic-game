using UnityEngine;
using System.Collections;

public class VisibleGeneric : MonoBehaviour {

	public void Visible(GameObject objeto)
	{
		objeto.SetActive (true);
	}
}
