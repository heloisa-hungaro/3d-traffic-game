using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class ConexaoGeneric : MonoBehaviour {

	private static string source;
	private static MySqlConnection conexao;
	public static string usuario;
	public static string coduser;


	public static MySqlConnection AbreConexao () {

		source = 	"Server=localhost;"+
			"Database=tacali;" +
			"User ID=heloisa;" +
			"Pooling=false;"+
			"Password=123;";

		return ConectarBanco (source);
	}

	public static MySqlConnection ConectarBanco(string _source){
		conexao = new MySqlConnection (_source);
		conexao.Open ();
		return conexao;
	}

	public static void FechaConexao(MySqlConnection _conexao){
		conexao.Close ();
		_conexao.Close ();
	}

	public void Offline(){
		coduser = "0";
	}
}
