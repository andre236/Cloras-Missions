using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour {

    [SerializeField]
    private float _raioInteracao = 1f;

    private int _geradorRecompensaAleatoria; // 1 a 100;
    private int _valorFinal;
  
    private bool _jogadorInteragir;

    [SerializeField]
    private bool _recompensaAleatoriaAtiva;

   

    private GameObject _jogadorGO;
    private GameObject _botaoInteracaoGO;
    [SerializeField]
    private GameObject[] _recompensas;

    /*1 - Balas de shotgun == + 3 pacotes = 50%
     *2 - Balas de Submachine == + 3 pacotes = 50%
     *3 - Coração = 5 corações = 50%
     *4 - Moedas = 10 moedas --- = 50%
     * 
     * Numero aleatórioA entre 1 a 100.
     * Se for 1 a 25 = Coração; 26 a 50 Moedas; Se for 51 a 70 = Shotgun; 71 a 100 Submachine
     * 
     * 
     * 
     */

    private Animator _bauAnimator;
    private AudioSource _bauAbrindoSom;
    private Transform _localDropItem;


    void OnEnable() {

        if (_recompensaAleatoriaAtiva) {
            _geradorRecompensaAleatoria = Random.Range(1, 100);
        }

        _bauAnimator = GetComponent<Animator>();
        _bauAbrindoSom = GameObject.Find("bauAbrindoSom").GetComponent<AudioSource>();
        _jogadorGO = GameObject.Find("Player");
        _botaoInteracaoGO = gameObject.transform.Find("interacaoE").gameObject;
        _botaoInteracaoGO.SetActive(false);
        _localDropItem = gameObject.transform.Find("posicaoDrop").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update() {
        DetectarJogador();
    }


    void DetectarJogador() {
        var diferencaParaJogadorX = _jogadorGO.gameObject.transform.position.x - transform.position.x;
        var diferencaParaJogadorY = _jogadorGO.gameObject.transform.position.y - transform.position.y;
        _jogadorInteragir = Mathf.Abs(diferencaParaJogadorX) < _raioInteracao && Mathf.Abs(diferencaParaJogadorY) < _raioInteracao;

        if (_jogadorInteragir) {
            _botaoInteracaoGO.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                _bauAbrindoSom.Play();
                _bauAnimator.SetTrigger("abrindo");

                if (_recompensaAleatoriaAtiva) {
                    Recompensas();
                } else {
                    Instantiate(_recompensas[0], _localDropItem.transform.position, Quaternion.identity);
                }
                
                Destroy(_botaoInteracaoGO);
                Destroy(this);
            }
        }
        else {
            _botaoInteracaoGO.SetActive(false);
        }


    }

    void Recompensas() {
        if(_geradorRecompensaAleatoria >= 51 && _geradorRecompensaAleatoria <= 70) {
            _valorFinal = 0; //Shotgun
            for(int i = 0; i < 3; i++) {
                Instantiate(_recompensas[_valorFinal], _localDropItem.transform.position, Quaternion.identity);
            }
        }
        if (_geradorRecompensaAleatoria >= 71 && _geradorRecompensaAleatoria <= 100) {
            _valorFinal = 1; //Submachine
            for (int i = 0; i < 3; i++) {
                Instantiate(_recompensas[_valorFinal], _localDropItem.transform.position, Quaternion.identity);
            }
        }
        if (_geradorRecompensaAleatoria >= 1 && _geradorRecompensaAleatoria <= 25) {
            _valorFinal = 2; // Corações
            for (int i = 0; i < 5; i++) {
                Instantiate(_recompensas[_valorFinal], _localDropItem.transform.position, Quaternion.identity);
            }
        }
        if (_geradorRecompensaAleatoria >= 26 && _geradorRecompensaAleatoria <= 50) {
            _valorFinal = 3; // Moedas
            for (int i = 0; i < 10; i++) {
                Instantiate(_recompensas[_valorFinal], _localDropItem.transform.position, Quaternion.identity);
            }
        }


    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), _raioInteracao);

    }

}
