using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class Sql : MonoBehaviour {

	private string source;
	private MySqlConnection conexao;

	// Use this for initialization
	void Start () {

		source = 	"Server=192.168.137.1;"+
					"Database=tacali;" +
					"User ID=heloisa;" +
					"Pooling=false;"+
					"Password=123;";

		ConectarBanco (source);
		Listar (conexao);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ConectarBanco(string _source){
		conexao = new MySqlConnection (_source);
		conexao.Open ();
	}

	void Listar(MySqlConnection _conexao){
		MySqlCommand comando = _conexao.CreateCommand ();
		comando.CommandText = "select usuario,senha from aluno";
		MySqlDataReader dados = comando.ExecuteReader ();

		while(dados.Read()){
			
			string usuario = dados.GetString ("usuario");
			string senha = dados.GetString ("senha");

			Debug.Log ("Usuario " + usuario);
			Debug.Log ("Senha " + senha);
		}
	}

}
