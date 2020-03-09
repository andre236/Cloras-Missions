using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transicoes : MonoBehaviour {


    private float _cooldownTransicaoAtual = 1f, _cooldownTransicaoInicial = 1f;
    private bool _cooldownTransicaoAtivo = false;

    [SerializeField]
    private Transform _ParaOnde;

    private BoxCollider2D _box2DTrigger;

    private Animator _animacaoCena;

    private CharPlayer _charPlayer;

    

    void Awake() {
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _box2DTrigger = GetComponent<BoxCollider2D>();
        _animacaoCena = GameObject.Find("TransicaoAnimacao").GetComponent<Animator>();
        _animacaoCena.gameObject.GetComponent<Image>().enabled = true;
    }


    void Update() {
        coolDownTransicao();
    }
    /*
    IEnumerator OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.CompareTag("Player")) {
            _animacaoCena.SetBool("estaTransitando", true);



            if (_animacaoCena.GetBool("estaTransitando") == true) {
                _player.SetEventoAtivoBool(true);
                yield return new WaitForSeconds(1);
                _player.gameObject.transform.position = _ParaOnde.transform.position;
                _player.SetEventoAtivoBool(false);
                _animacaoCena.SetBool("estaTransitando", false);

                //WaitForSecondsRealtime.Equals(1f, 2f);
            }
            
        }
    }
    */

    void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.gameObject.CompareTag("Player")) {
            _animacaoCena.SetBool("estaTransitando", true);
            
            if (_animacaoCena.GetBool("estaTransitando")) {
                _cooldownTransicaoAtivo = true;
                _charPlayer.EventoAtivo = true;


            }

        }
    }
    
    void coolDownTransicao() {
        if (_cooldownTransicaoAtivo) {
            _cooldownTransicaoAtual -= Time.deltaTime;
            if(_cooldownTransicaoAtual <= 0) {
                _cooldownTransicaoAtual = 0;
                _charPlayer.gameObject.transform.position = _ParaOnde.transform.position;
                _animacaoCena.SetBool("estaTransitando", false);
                _charPlayer.EventoAtivo = false;
                _cooldownTransicaoAtivo = false;
                _cooldownTransicaoAtual = _cooldownTransicaoInicial;
            }
        }
    }

}
