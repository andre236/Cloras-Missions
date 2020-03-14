using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morcego : MonoBehaviour {

    private float _vidaTotal = 2;
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

    private Animator _morcegoAnimacao;

    private CharPlayer _charPlayer;
    private CameraShake _cameraShake;


    public float VidaAtual { get; private set; }

    public bool EstaVoando { get; set; }

    public AudioSource[] MorcegoSons { get; set; }

    void Awake() {
        _barraVida = Instantiate(_barraVidaPrefab, FindObjectOfType<Canvas>().transform.Find("barrasDeVida").transform).GetComponent<Image>();
        _barraVidaCheia = new List<Image>(_barraVida.GetComponentsInChildren<Image>()).Find(img => img != _barraVida);

        _rbMorcego = GetComponent<Rigidbody2D>();
        _morcegoCollider = GetComponent<CircleCollider2D>();

        _morcegoAnimacao = GetComponent<Animator>();

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();

        MorcegoSons = transform.Find("morcegoSons").GetComponentsInChildren<AudioSource>();

    }

    void Start() {
        VidaAtual = _vidaTotal;
    }

    void FixedUpdate() {
        _rbMorcego.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.gameObject.transform.position, _velocidadeMovimento * Time.fixedDeltaTime));
        Interface();
    }

    public void InfligirDano(int danoInfligido) {
        VidaAtual -= danoInfligido;
        _morcegoAnimacao.SetTrigger("recebendoDano");
        MorcegoSons[4].Play();

        if(VidaAtual <= 0){
            Morrer();
        }
    }

    void Interface() {
        float altura = 0.5f;

        //Barra de vida posição
        _barraVida.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, altura, 0));
        _barraVidaCheia.fillAmount = VidaAtual / _vidaTotal;

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

    void Morrer() {
        transform.position = new Vector2(transform.position.x, transform.position.y);
        MorcegoSons[0].Stop();
        MorcegoSons[3].Play();
        MorcegoSons[2].Play();

        _charPlayer.AdicionarAlmas(1);
        _cameraShake.Shake(0.1f, 0.1f);
        //MorcegoSons.SetBool("estaMorrendo", true);

        GetComponent<SpriteRenderer>().sortingOrder = 3;

       
        Destroy(_barraVida.gameObject);
        

        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(this);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), _alcanceVisao);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bala")) {
            _verPlayer = true;
            InfligirDano(_charPlayer.BalaPistolDano);
            Destroy(collision.gameObject);
            
        }
    }

}
