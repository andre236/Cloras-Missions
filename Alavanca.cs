using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alavanca : MonoBehaviour {

    private bool _estaInteragindo;
    private float _raioInteracao = 1.5f;

    private GameObject _interacaoEGO;
    private GameObject _triggerAlavanca;

    private Animator _alavancaAnim;
  

    private PonteMov _ponteEvento;
    private CharPlayer _charPlayer;


    

    void Awake() {
        _interacaoEGO = gameObject.transform.Find("interacaoE").gameObject;
        _interacaoEGO.SetActive(false);
        _triggerAlavanca = gameObject.transform.Find("TriggerAlavanca").gameObject;
        _alavancaAnim = GetComponent<Animator>();

        _ponteEvento = gameObject.transform.Find("TriggerAlavanca").gameObject.GetComponent<PonteMov>();
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
     
      
    
    }

    // Update is called once per frame
    void Update() {
        DetectarJogador();
    }

    void DetectarJogador() {
        var diferencaJogadorX = transform.position.x - _charPlayer.gameObject.transform.position.x;
        var diferencaJogadorY = transform.position.y - _charPlayer.gameObject.transform.position.y;
        _estaInteragindo = Mathf.Abs(diferencaJogadorX) < _raioInteracao && Mathf.Abs(diferencaJogadorY) < _raioInteracao;

        if (_estaInteragindo) {
            _interacaoEGO.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                //Som Da Alavanca;
                _alavancaAnim.SetBool("estaAtiva", true);
                _ponteEvento.MovimentarPonte();
                Destroy(_interacaoEGO);
                Destroy(this);


            }
        } else {
            _interacaoEGO.SetActive(false);
        }

    }

  

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), _raioInteracao);
    }
}
