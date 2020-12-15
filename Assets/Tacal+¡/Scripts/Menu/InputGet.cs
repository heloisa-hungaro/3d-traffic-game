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
	public GameObject usuarioSenhaError;

	// Use this for initialization
	public void ShowInput () {
		conexao = ConexaoGeneric.AbreConexao();
		Listar (conexao);
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
			int tipo = dados.GetInt32 ("tipo");
			ConexaoGeneric.usuario = dados.GetString("usuario");
			ConexaoGeneric.coduser = dados.GetString ("codigo");

			if(tipo == 1){ //Tipo 1 = aluno
				
				SceneManager.LoadScene (1); //Numero 1 = Scene Game
			}

			else{//Tipo 0 = professor
				SceneManager.LoadScene(2); //Numero 2 = Scene ProfessorMenu
			}

		} 

		else {		
			usuarioSenhaError.SetActive(true);

		}

		ConexaoGeneric.FechaConexao (conexao);

	}



			



}
