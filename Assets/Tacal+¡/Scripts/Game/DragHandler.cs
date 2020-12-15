using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Transform startParent;
	string nomeobjeto1 = "";
	static int contafinal = 0;
	static int makesure = 0;
	public static int score=0; //marca a pontuaçao


	public GameObject quadradoImage;
	public GameObject trianguloImage;
	public GameObject hexagonoImage;
	public GameObject pentagonoImage;

	public GameObject quadradoSlot;
	public GameObject trianguloSlot;
	public GameObject pentagonoSlot;
	public GameObject hexagonoSlot;



	public static GameObject triangulo1;
	public static GameObject triangulo2;
	public static GameObject triangulo3;
	public static GameObject triangulo4;
	public static GameObject triangulo5;
	public static GameObject quadrado1;
	public static GameObject quadrado2;
	public static GameObject quadrado3;
	public static GameObject quadrado4;
	public static GameObject quadrado5;
	public static GameObject pentagono1;
	public static GameObject pentagono2;
	public static GameObject pentagono3;
	public static GameObject pentagono4;
	public static GameObject pentagono5;
	public static GameObject hexagono1;
	public static GameObject hexagono2;
	public static GameObject hexagono3;
	public static GameObject hexagono4;
	public static GameObject hexagono5;







	static Vector3 quad;
	static Vector3 tri;
	static Vector3 pen;
	static Vector3 hex;


	public void OnBeginDrag (PointerEventData eventData)
	{
		//salvando as posiçoes das images e garantindo que isso aconteça apenas uma vez
		if (makesure == 0) {
			quad = quadradoImage.transform.position;
			tri = trianguloImage.transform.position;
			pen = pentagonoImage.transform.position;
			hex = hexagonoImage.transform.position;
			makesure = 1;
		}


		nomeobjeto1 = this.gameObject.name; //nome do objeto sendo arrastado
		itemBeingDragged = gameObject;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
		




	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}


	//funçao que troca as imagens
	public void Swap(GameObject x, GameObject y){
		Vector3 keepimage = x.transform.position;
		x.transform.position = y.transform.position;
		y.transform.position = keepimage;
	}

	//funçao que restaura o nome original dos objects, para que nao haja confusao ao trocar
	public void Restaura(){
		quadradoSlot.name = "QuadradoSlot";
		trianguloSlot.name = "TrianguloSlot";
		hexagonoSlot.name = "HexagonoSlot";
		pentagonoSlot.name = "PentagonoSlot";


	}



	public void OnEndDrag (PointerEventData eventData)
	{
		 triangulo1 = GameObject.Find ("Triangulo1");
		 triangulo2 = GameObject.Find ("Triangulo2");
		 triangulo3 = GameObject.Find ("Triangulo3");
		 triangulo4 = GameObject.Find ("Triangulo4");
		 triangulo5 = GameObject.Find ("Triangulo5");

		 quadrado1 = GameObject.Find ("Quadrado1");
		 quadrado2 = GameObject.Find ("Quadrado2");
		 quadrado3 = GameObject.Find ("Quadrado3");
		 quadrado4 = GameObject.Find ("Quadrado4");
		 quadrado5 = GameObject.Find ("Quadrado5");

		 pentagono1 = GameObject.Find ("Pentagono1");
		 pentagono2 = GameObject.Find ("Pentagono2");
		 pentagono3 = GameObject.Find ("Pentagono3");
		 pentagono4 = GameObject.Find ("Pentagono4");
		 pentagono5 = GameObject.Find ("Pentagono5");

		 hexagono1 = GameObject.Find ("Hexagono1");
		 hexagono2 = GameObject.Find ("Hexagono2");
		 hexagono3 = GameObject.Find ("Hexagono3");
		 hexagono4 = GameObject.Find ("Hexagono4");
		 hexagono5 = GameObject.Find ("Hexagono5");


		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent != startParent) { //se parou de movimentar

			if (this.gameObject.name.Contains ("Quadrado")) { //se o objeto sendo arrastado for um quadrado:

				if (this.transform.parent.name == "QuadradoSlot") { //se o lugar que ele esta for o lugar do quadrado:
					this.gameObject.SetActive (false); //quadrado some
					//quadradoImage.gameObject.SetActive (false); //image quadrado some

					score += 40;
					contafinal++;
					this.transform.parent = startParent; // reposiciono o objeto para o seu local original (para que quando o contafinal = 4, ele ja esteja posicionado)
				}
			}

			if (this.gameObject.name.Contains ("Triangulo")) {

				if (this.transform.parent.name == "TrianguloSlot") {
					this.gameObject.SetActive (false); //triangulo some
					//image triangulo some
					//trianguloImage.gameObject.SetActive (false);

					score += 30;
					contafinal++;
					transform.parent = startParent;
				}
			}

			if (this.gameObject.name.Contains ("Pentagono")) {

				if (this.transform.parent.name == "PentagonoSlot") {
					this.gameObject.SetActive (false); //pentagono some
					//image pentagono some
					//pentagonoImage.gameObject.SetActive (false);

					score += 50;
					contafinal++;
					transform.parent = startParent;
				}
			}



			if (this.gameObject.name.Contains ("Hexagono")) {

				if (this.transform.parent.name == "HexagonoSlot") {
					this.gameObject.SetActive (false); //hexagono some
					//image hexagono some
					//hexagonoImage.gameObject.SetActive (false);

					score += 60;
					contafinal++;
					transform.parent = startParent;
				}
			}



			transform.parent = startParent; // caso o jogador erre o local, o objeto volta para a posiçao original


			if (contafinal == 20) {

				quadradoImage.transform.position = quad;
				trianguloImage.transform.position = tri;
				pentagonoImage.transform.position = pen;
				hexagonoImage.transform.position = hex;

				/*
				//declarando e encontrando gameobjects adicionais
				GameObject quad2 = GameObject.Find("Figura1 (2)").SetActive(true);
				GameObject quad3 = GameObject.Find("Figura1 (3)").SetActive(true);
				GameObject quad4 = GameObject.Find("Figura1 (4)").SetActive(true);
				GameObject quad5 = GameObject.Find("Figura1 (5)").SetActive(true);

				GameObject tri2 = GameObject.Find("Figura2 (2)").SetActive(true);
				GameObject tri3 = GameObject.Find("Figura2 (3)").SetActive(true);
				GameObject tri4 = GameObject.Find("Figura2 (4)").SetActive(true);
				GameObject tri5 = GameObject.Find("Figura2 (5)").SetActive(true);

				GameObject pen2 = GameObject.Find("Figura3 (2)").SetActive(true);
				GameObject pen3 = GameObject.Find("Figura3 (3)").SetActive(true);
				GameObject pen4 = GameObject.Find("Figura3 (4)").SetActive(true);
				GameObject pen5 = GameObject.Find("Figura3 (5)").SetActive(true);

				GameObject hex2 = GameObject.Find("Figura4 (2)").SetActive(true);
				GameObject hex3 = GameObject.Find("Figura4 (3)").SetActive(true);
				GameObject hex4 = GameObject.Find("Figura4 (4)").SetActive(true);
				GameObject hex5 = GameObject.Find("Figura4 (5)").SetActive(true);

				*/


			

				//final da ronda
				//coloca os objetos no centro da tela
				//troca a posiçao dos slots e das images atreladas a eles

				int random = Random.Range (1, 4); //variavel que recebe um valor aleatorio e cria as possiveis alteraçoes na fase

				if (random == 1) {
					Restaura ();
					//troco os objects
					quadradoSlot.name = "TrianguloSlot";
					trianguloSlot.name = "QuadradoSlot";
					hexagonoSlot.name = "PentagonoSlot";
					pentagonoSlot.name = "HexagonoSlot";
					//troco as imagens
					Swap (quadradoImage, trianguloImage);
					Swap (pentagonoImage, hexagonoImage);
				}


				if (random == 2) {
					Restaura ();
					//troco os objects
					quadradoSlot.name = "PentagonoSlot";
					trianguloSlot.name = "HexagonoSlot";
					hexagonoSlot.name = "TrianguloSlot";
					pentagonoSlot.name = "QuadradoSlot";
					//troco as imagens
					Swap (quadradoImage, pentagonoImage);
					Swap (trianguloImage, hexagonoImage);
				}

				if (random == 3) {
					Restaura ();
					//troco os objects
					quadradoSlot.name = "HexagonoSlot";
					trianguloSlot.name = "PentagonoSlot";
					hexagonoSlot.name = "QuadradoSlot";
					pentagonoSlot.name = "TrianguloSlot";
					//troco as imagens
					Swap (quadradoImage, hexagonoImage);
					Swap (trianguloImage, pentagonoImage);
				}
					

				//quadradoImage.SetActive (true);
				//trianguloImage.SetActive (true);
				//pentagonoImage.SetActive (true);
				//hexagonoImage.SetActive (true);

				//ativa as figuras


				triangulo1.SetActive (true);
				triangulo2.SetActive (true);
				triangulo3.SetActive (true);
				triangulo4.SetActive (true);
				triangulo5.SetActive (true);

				quadrado1.SetActive (true);
				quadrado2.SetActive (true);

				quadrado3.SetActive (true);
				quadrado4.SetActive (true);
				quadrado5.SetActive (true);
				pentagono1.SetActive (true);
				pentagono2.SetActive (true);
				pentagono3.SetActive (true);
				pentagono4.SetActive (true);
				pentagono5.SetActive (true);
				hexagono1.SetActive (true);
				hexagono2.SetActive (true);
				hexagono3.SetActive (true);
				hexagono4.SetActive (true);
				hexagono5.SetActive (true);





				/*

				foreach (GameObject a in quadrado)
					a.SetActive (true);
				foreach (GameObject b in triangulo)
					b.SetActive (true);
				foreach (GameObject c in pentagono)
					c.SetActive (true);
				foreach (GameObject d in hexagono)
					d.SetActive (true);

*/
				
				

			}

			//transform.position = startPosition;
		}
	}

}
