using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour {



    private GameObject _creditosGO, _opcoesGO, _menuInicialGO;
    private Animator _confirmandoModoAnim, _transicaoAnima;

    [SerializeField]
    private Button[] _menuBts;

    private AudioSource[] _sonsFXopcoes; // 0 = Selecionar; 1 = Confirmar; 2 = Recusar, 3 = Entrando em algum modo.;


    private Text _startTXT;


    void Start() {
        // # Referenciando em Cena;

        _menuBts = GameObject.Find("Canvas").transform.Find("MenuOpcoes").GetComponentsInChildren<Button>();
        _creditosGO = GameObject.Find("Canvas").transform.Find("Janelas").transform.Find("Creditos").gameObject;
        _opcoesGO = GameObject.Find("Canvas").transform.Find("Janelas").transform.Find("OpcoesVolume").gameObject;
        _menuInicialGO = GameObject.Find("MenuOpcoes");

        _confirmandoModoAnim = GameObject.Find("MenuOpcoes").transform.Find("LEVEL ATTACK TXT").GetComponent<Animator>();
        _transicaoAnima = GameObject.Find("Canvas").transform.Find("TransicaoIMG").GetComponent<Animator>();


        _sonsFXopcoes = GameObject.Find("Canvas").transform.Find("SoundsEffectGame").GetComponentsInChildren<AudioSource>();
        
        _startTXT = GameObject.Find("Canvas").transform.Find("StartTXT").GetComponent<Text>();

        // # GameObjects que irão iniciar ativados e desativados;

        _creditosGO.SetActive(false);
        _opcoesGO.SetActive(false);
        _menuInicialGO.SetActive(false);
        _transicaoAnima.gameObject.SetActive(false);
        _startTXT.gameObject.SetActive(true);


    }


    void Update() {
        Comandos();
        Ativacoes();
    }


    void Comandos() {
        var comandoFechar = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace);
        var janelas = _opcoesGO.activeSelf || _creditosGO.activeSelf;


        // Comando para sair: Esc, Backspace.
        if (comandoFechar && janelas) {
            _sonsFXopcoes[2].Play();
            _opcoesGO.SetActive(false);
            _creditosGO.SetActive(false);
            _menuInicialGO.SetActive(true);

            //Sair
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            //Confirmar
        }

        // Tela "Press Any Button to Start"
        if (Input.anyKeyDown && _startTXT.gameObject.activeSelf) {
            _sonsFXopcoes[1].Play();
            _startTXT.gameObject.SetActive(false);
            _menuInicialGO.SetActive(true);
        }

    }

    void Ativacoes() {

        if (_opcoesGO.activeSelf) {
            _creditosGO.SetActive(false);
            _menuInicialGO.SetActive(false);
        }
        else if (_creditosGO.activeSelf) {
            _menuInicialGO.SetActive(false);
            _opcoesGO.SetActive(false);
        }
        else {
            _opcoesGO.SetActive(false);
            _creditosGO.SetActive(false);
        }

    }

  

    public void IniciarLevelAttack() {
        _sonsFXopcoes[1].Play();
        _confirmandoModoAnim.SetTrigger("Confirmando");
        _transicaoAnima.gameObject.SetActive(true);
        StartCoroutine("CooldownTransicaoDeCenaLevelAttack");
    }

    public void SairDoJogo() {
        Application.Quit();
    }

    IEnumerator CooldownTransicaoDeCenaLevelAttack() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelAttack");
    }
}
