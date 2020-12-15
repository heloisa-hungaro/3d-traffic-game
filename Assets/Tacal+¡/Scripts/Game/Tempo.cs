using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class Tempo : MonoBehaviour {

	public static float myTimer;
	public Text timerText;
	public static bool timerIsActive = true;
	public GameObject finalPanel;
	private static MySqlConnection conexao;




	void Start () {
		timerText.GetComponent<Text> ();
		myTimer = 30;
		timerIsActive = true;
		DragHandler.score = 0;
	}



	
	void Update () {
		
		if (timerIsActive) { //se a variavel for true significa que mytimer>0, se for false ela para de contar e mytimer = 0, para a contagem
			myTimer -= Time.deltaTime;
			string minutes = ((int)myTimer / 60).ToString ("f0"); //converter os minutos
			string seconds = (myTimer % 60).ToString ("f2"); //converter os segundos
			timerText.text = "Tempo:          " + minutes + ":" + seconds;

			if(myTimer <= 0){
				myTimer = 0;
				timerIsActive = false;
				timerText.text = "Tempo:          0:00:00";
				finalPanel.SetActive (true);

				if (ConexaoGeneric.coduser == "0")
					return;


				conexao = ConexaoGeneric.AbreConexao ();

			//	Debug.Log (ConexaoGeneric.coduser);

				MySqlCommand comando = conexao.CreateCommand ();
				comando.CommandText = "select codigo from aluno where cod_usuario = " + ConexaoGeneric.coduser;
				MySqlDataReader dados = comando.ExecuteReader ();
				dados.Read ();
				string codigo = dados.GetString ("codigo");
				string data = System.DateTime.Now.ToString ("yyyy-MM-dd");


				dados.Close ();
				comando.CommandText= "insert into estatisticas (pontuacao,tempo,jogadas,erros,data,nivel,cod_aluno) values (" + 
					DragHandler.score + ", '00:30:00', 0, 0, " + ((char)39) + data + ((char)39) + ", 1, " + codigo + ")";
				dados = comando.ExecuteReader ();
				dados.Close ();

				ConexaoGeneric.FechaConexao (conexao);

			
				return;
				//criar janela dizendo que o tempo acabou e mostrando a pontuação
			}
		}


	}
}
