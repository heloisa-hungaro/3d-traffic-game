using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class InputGet : MonoBehaviour {

	private InputField inputFieldUser;
	private InputField inputFieldSenha;
	private string source;
	private MySqlConnection conexao;

	// Use this for initialization
	public void ShowInput () {
		AbreConexao ();

	}





	// Use this for initialization
	void AbreConexao () {

		source = 	"Server=192.168.137.1;"+
			"Database=tacali;" +
			"User ID=heloisa;" +
			"Pooling=false;"+
			"Password=123;";

		ConectarBanco (source);
		Listar(conexao);

	}
		

	void ConectarBanco(string _source){
		conexao = new MySqlConnection (_source);
		conexao.Open ();
	}


	public void Listar(MySqlConnection _conexao){
		
		//INPUT USER
		GameObject inputUsuario = GameObject.Find("FieldUsuario");
		InputField inputFieldUser = inputUsuario.GetComponent<InputField>();

		//INPUT SENHA
		GameObject inputSenha = GameObject.Find("FieldSenha");
		InputField inputFieldSenha = inputSenha.GetComponent<InputField>();


		MySqlCommand comando = _conexao.CreateCommand ();
		comando.CommandText = "select * from usuario where usuario=" + ((char)39) + inputFieldUser.text +
			((char)39) + " and senha=md5(" + ((char)39) + inputFieldSenha.text +((char)39) + ")";
		MySqlDataReader dados = comando.ExecuteReader ();

		dados.Read ();

		if (dados.HasRows) {
			string codigo = dados.GetString ("codigo");
			bool tipo = dados.GetBoolean ("tipo");
			Debug.Log ("teste");

			if(tipo){ //Tipo 1 = aluno
				SceneManager.LoadScene (1); //Numero 1 = Scene Game
			}

			else{//Tipo 0 = professor
				SceneManager.LoadScene(2); //Numero 2 = Scene ProfessorMenu
			}

		} 

		else {
			Debug.Log ("Usuario nao existe");
			//Criar um texto avisando que usuario nao exite
		}

	}



			



}
