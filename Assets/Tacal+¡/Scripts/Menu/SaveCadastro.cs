using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class SaveCadastro : MonoBehaviour {

	private InputField fieldUsuario;
	private InputField fieldSenha;
	private InputField fieldNome;
	private InputField fieldEmail;
	public GameObject emptyError;
	public GameObject alunoCadastradoError;
	public GameObject turmaNaoExisteError;
	public GameObject profNomeExisteError;
	public GameObject turmaNaoEncontradaError;
	public GameObject turmaExisteError;
	public GameObject usuarioNomeExisteError;
	public GameObject cadastroSucess;
	public GameObject turmaNomeExisteError;
	public GameObject profNaoExisteError;	
	public GameObject atribuicaoExisteError;
	public GameObject atribuicaoSucess;
	public GameObject estat;

	private string source;
	private MySqlConnection conexao;


	public void CadastroProf(){

		GameObject inputUsuario = GameObject.Find ("FieldUsuario");
		InputField fieldUsuario = inputUsuario.GetComponent<InputField> ();

		GameObject inputSenha = GameObject.Find ("FieldSenha");
		InputField fieldSenha = inputSenha.GetComponent<InputField> ();

		GameObject inputNome = GameObject.Find ("FieldNome");
		InputField fieldNome = inputNome.GetComponent<InputField> ();

		GameObject inputEmail = GameObject.Find ("FieldEmail");
		InputField fieldEmail = inputEmail.GetComponent<InputField> ();

		if (fieldUsuario.text == "" || fieldSenha.text == "" || fieldNome.text == "" || fieldEmail.text == "") {
			emptyError.SetActive (true);
			return;
		}
			
		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText= "select usuario from usuario where lower(usuario) = lower(" + ((char)39) + fieldUsuario.text + ((char)39) + ")";
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			//erro: usuario já existe
			usuarioNomeExisteError.SetActive (true);
			dados.Close ();
			ConexaoGeneric.FechaConexao (conexao);
			return;
		} 

		dados.Close ();
		comando.CommandText = "select nome from professor where lower(nome) = lower(" + ((char)39) + fieldNome.text + ((char)39) + ")";
		dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			//erro: nome de prof já existe
			profNomeExisteError.SetActive(true);
			dados.Close ();
			ConexaoGeneric.FechaConexao (conexao);
			return;
		}

		dados.Close ();
		comando.CommandText = "insert into usuario (usuario,senha,tipo) values (" + ((char)39) + fieldUsuario.text + ((char)39) + ", md5(" + ((char)39) + fieldSenha.text + ((char)39) + "), 0 )";
		dados = comando.ExecuteReader ();

		dados.Close ();
		comando.CommandText = "select codigo from usuario where lower (usuario) = lower (" + ((char)39) + fieldUsuario.text + ((char)39) + ")";
		dados = comando.ExecuteReader ();
		dados.Read ();
		string codigouser = dados.GetString ("codigo");

		dados.Close ();
		comando.CommandText = "insert into professor (nome,email,cod_usuario) values (" + ((char)39) + fieldNome.text + ((char)39) + ", " + ((char)39) + fieldEmail.text + ((char)39) + ", " + codigouser + ")";
		dados = comando.ExecuteReader ();
		dados.Close ();

		//cadastrado com sucesso
		cadastroSucess.SetActive(true);

		fieldEmail.text = "";
		fieldNome.text = "";
		fieldSenha.text = "";
		fieldUsuario.text = "";

		GameObject abaprof = GameObject.Find ("AbaPanelProfessor");
		abaprof.SetActive (false);

		ConexaoGeneric.FechaConexao (conexao);

	} //fim cadastro prof
		

	public void CadastroTurma(){
		
		GameObject inputNome = GameObject.Find ("FieldNome");
		InputField fieldNome = inputNome.GetComponent<InputField> ();

		GameObject inputAno = GameObject.Find ("FieldAno");
		InputField fieldAno = inputAno.GetComponent<InputField> ();

		if (fieldNome.text == "" || fieldAno.text == "") {
			emptyError.SetActive (true);
			return;
		}

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "select * from turma where ano = " + fieldAno.text + " and lower(nome)=lower(" + ((char)39) + fieldNome.text + ((char)39) + ")";
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			//erro: turma já existe
			turmaExisteError.SetActive(true);
			dados.Close ();
			ConexaoGeneric.FechaConexao (conexao);
			return;
		} 
			
		dados.Close ();
		comando.CommandText = "insert into turma (ano,nome) values (" + fieldAno.text + ", " + ((char)39) + fieldNome.text + ((char)39) + ")";
		dados = comando.ExecuteReader ();
		dados.Close ();

		//cadastrado com sucesso
		cadastroSucess.SetActive(true);

		fieldAno.text = "";
		fieldNome.text = "";

		GameObject abaturma = GameObject.Find ("AbaPanelTurma");
		abaturma.SetActive (false);

		ConexaoGeneric.FechaConexao (conexao);

	} //fim cadastro turma


	public void CarregaTurmas(){
		
		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();

		GameObject buttonTurma = GameObject.Find ("Turma");
		Button btnTurma = buttonTurma.GetComponent<Button> ();

		dropTurma.options.Clear ();
		dropTurma.ClearOptions ();

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "select *, concat(nome," + ((char)39) + "/" + ((char)39) + ",ano) as lista from turma order by ano desc, nome";
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			do {
			//	Debug.Log ("x");
				string turma = dados.GetString ("lista");
				dropTurma.options.Add (new Dropdown.OptionData () { text = turma });

			} while (dados.Read ());

			dropTurma.value = 1;
			dropTurma.value = 0;

		}
		else {
			
			//erro: não existem turmas cadastradas
			dados.Close();
			ConexaoGeneric.FechaConexao (conexao);
			turmaNaoExisteError.SetActive(true);
			//if(estat.active == false)
				btnTurma.onClick.Invoke(); //redirecionar para a pagina de cadastro de turma
			return;
		}
		//Debug.Log (dropTurma.options[dropTurma.value].text);

		ConexaoGeneric.FechaConexao (conexao);

	} // fim carrega turmas


	public void CarregaProfessores(){
		
		GameObject dropDownProfessor = GameObject.Find ("DropDownProfessor");
		Dropdown dropProfessor = dropDownProfessor.GetComponent<Dropdown> ();

		GameObject buttonProf = GameObject.Find ("Professor");
		Button btnProf = buttonProf.GetComponent<Button> ();

		dropProfessor.options.Clear ();
		dropProfessor.ClearOptions ();

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "select codigo, nome from professor order by nome";
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();
		if (dados.HasRows) {
			do {
				string professor = dados.GetString ("nome");
				dropProfessor.options.Add (new Dropdown.OptionData () { text = professor });

			} while (dados.Read ());

			dropProfessor.value = 1;
			dropProfessor.value = 0;

		} 
		else {
			//erro: não existem profs cadastrados
			dados.Close();
			ConexaoGeneric.FechaConexao (conexao);
			profNaoExisteError.SetActive(true);
			btnProf.onClick.Invoke(); //redirecionar para a pagina de cadastro de prof
			return;

		}

		ConexaoGeneric.FechaConexao (conexao);

	} // fim carrega profs



	public void CadastroAtribuicao(){

		string codigoprof = "";
		string codigoturma = "";

		GameObject dropDownProfessor = GameObject.Find ("DropDownProfessor");
		Dropdown dropProfessor = dropDownProfessor.GetComponent<Dropdown> ();

		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "select codigo from turma where concat(nome," + ((char)39) + "/" + ((char)39) + ",ano) = " + ((char)39) + dropTurma.options [dropTurma.value].text + ((char)39);
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			codigoturma = dados.GetString ("codigo");
			dados.Close ();
		} 
		else {
			//erro: não existem turmas cadastradas
			dados.Close ();
			turmaNaoEncontradaError.SetActive(true);
			ConexaoGeneric.FechaConexao (conexao);
			return;
		}

		comando.CommandText = "select codigo from professor where nome = " + ((char)39) + dropProfessor.options [dropProfessor.value].text + ((char)39);
		dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			codigoprof = dados.GetString ("codigo");
			dados.Close ();
		} 
		else {
			//erro: não existem profs cadastrados
			dados.Close ();
			profNaoExisteError.SetActive(true);
			ConexaoGeneric.FechaConexao (conexao);
			return;
		}

		comando.CommandText = "select * from turma_professor where cod_turma = " + codigoturma + " and cod_professor = " + codigoprof;
		dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			//erro: essa atribuição já existe
			dados.Close();
			atribuicaoExisteError.SetActive(true);
			ConexaoGeneric.FechaConexao (conexao);
			return;
		} 

		dados.Close ();
		comando.CommandText = "insert into turma_professor (cod_professor,cod_turma) values (" + codigoprof + ", " + codigoturma + ")";
		dados = comando.ExecuteReader ();
		dados.Close ();

		//cadastrado com sucesso
		atribuicaoSucess.SetActive(true);

		GameObject abaatribuicao = GameObject.Find ("AbaPanelAtribuicao");
		abaatribuicao.SetActive (false);

		ConexaoGeneric.FechaConexao (conexao);
	
	} //fim cadastro atribuição


	public void CadastroAluno(){

		string codigoturma="";
		string codigouser="";

		GameObject inputUsuario = GameObject.Find ("FieldUsuario");
		InputField fieldUsuario = inputUsuario.GetComponent<InputField> ();

		GameObject inputSenha = GameObject.Find ("FieldSenha");
		InputField fieldSenha = inputSenha.GetComponent<InputField> ();

		GameObject inputNome = GameObject.Find ("FieldNome");
		InputField fieldNome = inputNome.GetComponent<InputField> ();

		GameObject inputRA = GameObject.Find ("FieldRA");
		InputField fieldRA = inputRA.GetComponent<InputField> ();

		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();


		if (fieldUsuario.text == "" || fieldSenha.text == "" || fieldNome.text == "" || fieldRA.text == "") {
			emptyError.SetActive (true);
			return;
		}

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText= "select * from turma";
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			dados.Close ();
			comando.CommandText= "select nome from aluno where lower(nome) = lower(" + ((char)39) + fieldNome.text + ((char)39) + ")";
			dados = comando.ExecuteReader ();
			dados.Read ();

			if (dados.HasRows) {
				//erro: aluno já cadastrado
				dados.Close ();
				alunoCadastradoError.SetActive(true);
				ConexaoGeneric.FechaConexao (conexao);
				return;
			} 

			dados.Close ();
			comando.CommandText= "select usuario from usuario where lower(usuario) = lower(" + ((char)39) + fieldUsuario.text + ((char)39) + ")";
			dados = comando.ExecuteReader ();
			dados.Read ();

			if (dados.HasRows) {
				//erro: usuário já existe
				dados.Close ();
				usuarioNomeExisteError.SetActive (true);
				ConexaoGeneric.FechaConexao (conexao);
				return;

			} 

			dados.Close ();
			comando.CommandText = "insert into usuario (usuario,senha,tipo) values (" + ((char)39) + fieldUsuario.text + ((char)39) + ", md5(" + ((char)39) + fieldSenha.text + ((char)39) + "), 1 )";
			dados = comando.ExecuteReader ();

			dados.Close ();
			comando.CommandText = "select codigo from usuario where lower (usuario) = lower (" + ((char)39) + fieldUsuario.text + ((char)39) + ")";
			dados = comando.ExecuteReader ();
			dados.Read ();
			codigouser = dados.GetString ("codigo");

			dados.Close ();
			comando.CommandText = "select codigo from turma where concat(nome," + ((char)39) + "/" + ((char)39) + ",ano) = " + ((char)39) + dropTurma.options [dropTurma.value].text + ((char)39);
			dados = comando.ExecuteReader ();
			dados.Read ();

			if (dados.HasRows) {
				codigoturma = dados.GetString ("codigo");
			} 
			else {
				dados.Close ();
				//erro: turma nao encontrada no banco de dados
				turmaNaoEncontradaError.SetActive(true);
				ConexaoGeneric.FechaConexao (conexao);
				return;
			}

			dados.Close ();
			comando.CommandText = "insert into aluno (nome,ra,cod_turma,cod_usuario) values (" + ((char)39) + fieldNome.text + ((char)39) + ", " + ((char)39) + fieldRA.text + ((char)39) + " , " + codigoturma + ", " + codigouser + ")";
			dados = comando.ExecuteReader ();
			dados.Close ();

			//cadastrado com sucesso
			cadastroSucess.SetActive(true);

			fieldNome.text = "";
			fieldRA.text = "";
			fieldSenha.text = "";
			fieldUsuario.text = "";

			GameObject abaaluno = GameObject.Find ("AbaPanelAluno");
			abaaluno.SetActive (false);

		} 
		else {
			//erro: não existem turmas cadastradas
			turmaNaoExisteError.SetActive(true);
			dados.Close ();
			ConexaoGeneric.FechaConexao (conexao);
			return;
		}	
	} //fim cadastro aluno

	public void CarregaEstTurma(){

		string nome;
		string pontua;
		string data;


		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();

		GameObject gnome1 = GameObject.Find ("Nome1");
		Text nome1 = gnome1.GetComponent<Text> ();
		GameObject gpontua1 = GameObject.Find ("Pontua1");
		Text pontua1 = gpontua1.GetComponent<Text> ();
		GameObject gdata1 = GameObject.Find ("Data1");
		Text data1 = gdata1.GetComponent<Text> ();

		GameObject gnome2 = GameObject.Find ("Nome2");
		Text nome2 = gnome2.GetComponent<Text> ();
		GameObject gpontua2 = GameObject.Find ("Pontua2");
		Text pontua2 = gpontua2.GetComponent<Text> ();
		GameObject gdata2 = GameObject.Find ("Data2");
		Text data2 = gdata2.GetComponent<Text> ();

		GameObject gnome3 = GameObject.Find ("Nome3");
		Text nome3 = gnome3.GetComponent<Text> ();
		GameObject gpontua3 = GameObject.Find ("Pontua3");
		Text pontua3 = gpontua3.GetComponent<Text> ();
		GameObject gdata3 = GameObject.Find ("Data3");
		Text data3 = gdata3.GetComponent<Text> ();

		GameObject gnome4 = GameObject.Find ("Nome4");
		Text nome4 = gnome4.GetComponent<Text> ();
		GameObject gpontua4 = GameObject.Find ("Pontua4");
		Text pontua4 = gpontua4.GetComponent<Text> ();
		GameObject gdata4 = GameObject.Find ("Data4");
		Text data4 = gdata4.GetComponent<Text> ();

		GameObject gnome5 = GameObject.Find ("Nome5");
		Text nome5 = gnome5.GetComponent<Text> ();
		GameObject gpontua5 = GameObject.Find ("Pontua5");
		Text pontua5 = gpontua5.GetComponent<Text> ();
		GameObject gdata5 = GameObject.Find ("Data5");
		Text data5 = gdata5.GetComponent<Text> ();




		nome1.text = "" ;
		pontua1.text = "";
		data1.text = "";
		nome2.text = "" ;
		pontua2.text = "";
		data2.text = "";
		nome3.text = "" ;
		pontua3.text = "";
		data3.text = "";
		nome4.text = "" ;
		pontua4.text = "";
		data4.text = "";
		nome5.text = "" ;
		pontua5.text = "";
		data5.text = "";

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();

		comando.CommandText= "select e.pontuacao, e.data, a.nome  from estatisticas e inner join aluno a on e.cod_aluno = a.codigo " +
							" inner join turma t on t.codigo = a.cod_turma where " +
			" concat(t.nome," + ((char)39) + "/" + ((char)39) + ",t.ano) = " +
			 ((char)39) + dropTurma.options [dropTurma.value].text + ((char)39) + " order by data desc, pontuacao desc";

		MySqlDataReader dados = comando.ExecuteReader ();

		if (dados.Read ()) {
			nome = dados.GetString ("nome");
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			nome1.text = nome ;
			pontua1.text = pontua;
			data1.text = data;
		}	
		if (dados.Read ()) {
			nome = dados.GetString ("nome");
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			nome2.text = nome ;
			pontua2.text = pontua;
			data2.text = data;
		}	
		if (dados.Read ()) {
			nome = dados.GetString ("nome");
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			nome3.text = nome ;
			pontua3.text = pontua;
			data3.text = data;
		}	
		if (dados.Read ()) {
			nome = dados.GetString ("nome");
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			nome4.text = nome ;
			pontua4.text = pontua;
			data4.text = data;
		}	
		if (dados.Read ()) {
			nome = dados.GetString ("nome");
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			nome5.text = nome ;
			pontua5.text = pontua;
			data5.text = data;
		}	

		dados.Close();
		ConexaoGeneric.FechaConexao(conexao);

	} // fim CarregaEstTurma

	public void CarregaAluno() {

		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();

		GameObject dropDownAluno = GameObject.Find ("DropDownAluno");
		Dropdown dropAluno = dropDownAluno.GetComponent<Dropdown> ();


		dropAluno.options.Clear ();
		dropAluno.ClearOptions ();

		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "select codigo from turma where concat(nome," + ((char)39) + "/" + ((char)39) + ",ano) = " + ((char)39) + dropTurma.options [dropTurma.value].text + ((char)39);
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Read ();
		string codturma = dados.GetString ("codigo");

		dados.Close();
		comando.CommandText = "select nome from aluno where cod_turma = " + codturma + " order by nome";
		dados = comando.ExecuteReader ();
		dados.Read ();

		if (dados.HasRows) {
			do {
				string aluno = dados.GetString ("nome");
				dropAluno.options.Add (new Dropdown.OptionData () { text = aluno });

			} while (dados.Read ());

			dropAluno.value = 1;
			dropAluno.value = 0;

		}
		else {

			//erro: não existem alunos cadastrados nessa turma
			dados.Close();
			ConexaoGeneric.FechaConexao (conexao);
			return;
		}
		//Debug.Log (dropTurma.options[dropTurma.value].text);

		ConexaoGeneric.FechaConexao (conexao);


	} // fim carrega aluno

	public void CarregaEstAluno(){
		
		string pontua;
		string data;


		GameObject dropDownTurma = GameObject.Find ("DropDownTurma");
		Dropdown dropTurma = dropDownTurma.GetComponent<Dropdown> ();

		GameObject dropDownAluno = GameObject.Find ("DropDownAluno");
		Dropdown dropAluno = dropDownAluno.GetComponent<Dropdown> ();

		GameObject gpontua1 = GameObject.Find ("Pontua1");
		Text pontua1 = gpontua1.GetComponent<Text> ();
		GameObject gdata1 = GameObject.Find ("Data1");
		Text data1 = gdata1.GetComponent<Text> ();

		GameObject gpontua2 = GameObject.Find ("Pontua2");
		Text pontua2 = gpontua2.GetComponent<Text> ();
		GameObject gdata2 = GameObject.Find ("Data2");
		Text data2 = gdata2.GetComponent<Text> ();

		GameObject gpontua3 = GameObject.Find ("Pontua3");
		Text pontua3 = gpontua3.GetComponent<Text> ();
		GameObject gdata3 = GameObject.Find ("Data3");
		Text data3 = gdata3.GetComponent<Text> ();

		GameObject gpontua4 = GameObject.Find ("Pontua4");
		Text pontua4 = gpontua4.GetComponent<Text> ();
		GameObject gdata4 = GameObject.Find ("Data4");
		Text data4 = gdata4.GetComponent<Text> ();

		GameObject gpontua5 = GameObject.Find ("Pontua5");
		Text pontua5 = gpontua5.GetComponent<Text> ();
		GameObject gdata5 = GameObject.Find ("Data5");
		Text data5 = gdata5.GetComponent<Text> ();

		GameObject gpontua6 = GameObject.Find ("Pontua6");
		Text pontua6 = gpontua6.GetComponent<Text> ();
		GameObject gdata6 = GameObject.Find ("Data6");
		Text data6 = gdata6.GetComponent<Text> ();

		GameObject gpontua7 = GameObject.Find ("Pontua7");
		Text pontua7 = gpontua7.GetComponent<Text> ();
		GameObject gdata7 = GameObject.Find ("Data7");
		Text data7 = gdata7.GetComponent<Text> ();

		GameObject gpontua8 = GameObject.Find ("Pontua8");
		Text pontua8 = gpontua8.GetComponent<Text> ();
		GameObject gdata8 = GameObject.Find ("Data8");
		Text data8 = gdata8.GetComponent<Text> ();




		pontua1.text = "";
		data1.text = "";
		pontua2.text = "";
		data2.text = "";
		pontua3.text = "";
		data3.text = "";
		pontua4.text = "";
		data4.text = "";
		pontua5.text = "";
		data5.text = "";
		pontua6.text = "";
		data6.text = "";
		pontua7.text = "";
		data7.text = "";
		pontua8.text = "";
		data8.text = "";


		conexao = ConexaoGeneric.AbreConexao ();

		MySqlCommand comando = conexao.CreateCommand ();

		comando.CommandText= "select e.pontuacao, e.data from estatisticas e inner join aluno a on e.cod_aluno = a.codigo " +
			" inner join turma t on t.codigo = a.cod_turma where a.nome = " +
			((char)39) + dropAluno.options [dropAluno.value].text + ((char)39) + " order by data desc, pontuacao desc";

		MySqlDataReader dados = comando.ExecuteReader ();

		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua1.text = pontua;
			data1.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua2.text = pontua;
			data2.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua3.text = pontua;
			data3.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua4.text = pontua;
			data4.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua5.text = pontua;
			data5.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua6.text = pontua;
			data6.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua7.text = pontua;
			data7.text = data;
		}	
		if (dados.Read ()) {
			pontua = dados.GetString ("pontuacao");
			data = dados.GetDateTime ("data").ToString("dd/MM/yyyy");
			pontua8.text = pontua;
			data8.text = data;
		}	

		dados.Close();
		ConexaoGeneric.FechaConexao(conexao);

	} // fim CarregaEstTurma

}





	

