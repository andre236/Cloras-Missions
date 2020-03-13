using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morcego : MonoBehaviour {

    private float _vidaTotal;
    private float _velocidadeMovimento = 5.0f;
    private float _alcanceVisao = 10f;

    private bool _vendoPlayer;
    private bool _verPlayer;
    

    [SerializeField]
    private GameObject _barraVidaPrefab;

    private Image _barraVida;
    private Image _barraVidaCheia;

    private Rigidbody2D _rbMorcego;
    private CircleCollider2D _morcegoCollider;

    private CharPlayer _charPlayer;


    public float VidaAtual { get; set; }

    public bool EstaVoando { get; set; }

    public AudioSource[] _morcegoSons { get; set; }

    void Awake() {
        _barraVida = Instantiate(_barraVidaPrefab, FindObjectOfType<Canvas>().transform.Find("barrasDeVida").transform).GetComponent<Image>();
        _barraVidaCheia = new List<Image>(_barraVida.GetComponentsInChildren<Image>()).Find(img => img != _barraVida);

        _rbMorcego = GetComponent<Rigidbody2D>();
        _morcegoCollider = GetComponent<CircleCollider2D>();
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();


    }

    void FixedUpdate() {
        _rbMorcego.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.gameObject.transform.position, _velocidadeMovimento));

    }

    void PerserguirPlayer() {
        var diferencaParaJogadorX = _charPlayer.gameObject.transform.position.x - transform.position.x;
        var diferencaParaJogadorY = _charPlayer.gameObject.transform.position.y - transform.position.y;
        _verPlayer = Mathf.Abs(diferencaParaJogadorX) < _alcanceVisao && Mathf.Abs(diferencaParaJogadorY) < _alcanceVisao;

        
        _vendoPlayer = (_verPlayer == true) ? _vendoPlayer = true : _vendoPlayer = false;

        
        if (_vendoPlayer) {
            _rbMorcego.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.gameObject.transform.position, _velocidadeMovimento));

        }

    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), _alcanceVisao);

    }

}
