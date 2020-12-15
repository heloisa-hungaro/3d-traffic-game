using UnityEngine;
using System.Collections;

public class InvisibleThings : MonoBehaviour {
	public GameObject cadastropanel;
	public GameObject abapanelprof;
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
	public GameObject estatisticas;
	public GameObject estatisticaspanel;

	void Awake () {

		//erros


		emptyError.SetActive (false);
		alunoCadastradoError.SetActive (false);
		turmaNaoExisteError.SetActive (false);
		profNomeExisteError.SetActive (false);
		turmaNaoEncontradaError.SetActive (false);
		turmaExisteError.SetActive (false);
		usuarioNomeExisteError.SetActive (false);
		cadastroSucess.SetActive (false);
		turmaNomeExisteError.SetActive (false);
		profNaoExisteError.SetActive (false);
		atribuicaoExisteError.SetActive (false);
		atribuicaoSucess.SetActive (false);

		abapanelprof.SetActive (false);
		cadastropanel.SetActive (false);
		estatisticaspanel.SetActive (false);

		if (ConexaoGeneric.usuario == "admin") {
			estatisticas.SetActive (false);
		} 
		else {
			estatisticas.SetActive (true);
		}


	}

}
