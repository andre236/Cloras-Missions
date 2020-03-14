using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morcego : MonoBehaviour {

    private float _vidaTotal = 2;
    private float _velocidadeMovimento = 5.0f;
    private float _alcanceVisao = 20f;
    
    private bool _vendoPlayer;
    
    [SerializeField]
    private GameObject _barraVidaPrefab;
    

    private Rigidbody2D _rbMorcego;
    private BoxCollider2D _morcegoCollider;
    private Animator _morcegoAnimacao;
    [SerializeField]
    private LayerMask _layersPermitidas;

    private Image _barraVida;
    private Image _barraVidaCheia;

    private CharPlayer _charPlayer;
    private CameraShake _cameraShake;


    public float VidaAtual { get; private set; }

    public bool EstaVoando { get; set; }

    public AudioSource[] MorcegoSons { get; set; }

    void Awake() {
        
        _rbMorcego = GetComponent<Rigidbody2D>();
        _morcegoCollider = GetComponent<BoxCollider2D>();
        _morcegoAnimacao = GetComponent<Animator>();

        MorcegoSons = transform.Find("morcegoSons").GetComponentsInChildren<AudioSource>();

        _barraVida = Instantiate(_barraVidaPrefab, FindObjectOfType<Canvas>().transform.Find("barrasDeVida").transform).GetComponent<Image>();
        _barraVidaCheia = new List<Image>(_barraVida.GetComponentsInChildren<Image>()).Find(img => img != _barraVida);

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();


    }

    void Start() {
        VidaAtual = _vidaTotal;

    }

    void FixedUpdate() {
        PerserguirPlayer();
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

        Vector2 direcao = new Vector2(_charPlayer.gameObject.transform.position.x - transform.position.x, _charPlayer.transform.position.y - transform.position.y);

        RaycastHit2D visaoInfo = Physics2D.Raycast(transform.position, direcao, _alcanceVisao, _layersPermitidas);
        
        if (visaoInfo.collider) {
            Debug.DrawLine(transform.position, visaoInfo.point, Color.green);

            //# Caso veja o player.
            if (visaoInfo.collider.CompareTag("Player")) {
                Debug.DrawLine(transform.position, visaoInfo.point, Color.red);
                _vendoPlayer = true;
            }

        }

        if (_vendoPlayer) {
            _rbMorcego.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.transform.position, _velocidadeMovimento * Time.fixedDeltaTime));
        }

        if (direcao.x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = false;
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

 

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bala")) {
            _vendoPlayer = true;
            InfligirDano(_charPlayer.BalaPistolDano);
            Destroy(collision.gameObject);
            
        }
    }

}
