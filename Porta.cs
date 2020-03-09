using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour {

    Animator _portaAnimator;
    AudioSource _portaoGrandeSom;

    [SerializeField]Vector2 _posicaoTrigger = new Vector2(0, -0.4f);
    [SerializeField]Vector2 _tamanhoCuboTrigger = new Vector2(2f, 1.6f);

    GameObject _jogadorGO;
    GameObject _BotaoInteracaoEGO;


    bool _jogadorInteragir;

    private void OnEnable() {
        _portaAnimator = GetComponent<Animator>();
        _jogadorGO = GameObject.FindGameObjectWithTag("Player");
        _BotaoInteracaoEGO = gameObject.transform.Find("interacaoE").gameObject;
        //_BotaoInteracaoEGO = GameObject.Find("interacaoE");
        _BotaoInteracaoEGO.SetActive(false);
        _portaoGrandeSom = GameObject.Find("PortaoGrandeSom").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        detectarJogador();

    }

    void detectarJogador() {
        var diferencaParaJogadorPosX = _jogadorGO.gameObject.transform.position.x - transform.position.x;
        var diferencaParaJogadorPosY = _jogadorGO.gameObject.transform.position.y - transform.position.y;
        _jogadorInteragir = Mathf.Abs(diferencaParaJogadorPosX) < _tamanhoCuboTrigger.x && Mathf.Abs(diferencaParaJogadorPosY) < _tamanhoCuboTrigger.y;

        if(_jogadorInteragir) {
            _BotaoInteracaoEGO.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)){
                _portaoGrandeSom.Play();
                _portaAnimator.SetBool("estaAberta", true);
                //Ação aqui
                if(this.gameObject.name== "PortaGrandeTESTE") {
                    _jogadorGO.transform.position = GameObject.Find("Telepmapa03").GetComponent<Transform>().transform.position;
                }
                Destroy(_BotaoInteracaoEGO);
                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(this);
            }
         
        } else {
            _BotaoInteracaoEGO.SetActive(false);
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + _posicaoTrigger.x, transform.position.y + _posicaoTrigger.y), _tamanhoCuboTrigger);
        
        
    }
}
