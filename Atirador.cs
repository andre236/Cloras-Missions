using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atirador : MonoBehaviour {

    private float _posYbarraVida = 0.5f;
    
    private Rigidbody2D _rbAtirador;
    
    private GameObject _armaDePatrulhaGO;  

    [SerializeField]
    private GameObject _prefabVidaBar;
    private GameObject[] _dropItensPrefab;

    private Image _barraVidaIMG;
    private Image _barraVidaCheiaIMG;
   
    private Animator _atiradorAnimacao;
    
    
    private CharPlayer _charPlayer;
    
    private CameraShake _cameraShake;
    private IAatirador _iaAtirador;


    public float VidaTotal { get; private set; } = 6;
    public float VidaAtual { get; private set; }

    public float VelocidadeMovimento { get; private set; } = 2.5f;

    public AudioSource[] AtiradorSons { get; set; }


    void Start() {
        VidaAtual = VidaTotal;
        _iaAtirador = GetComponent<IAatirador>();
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();

        _barraVidaIMG = Instantiate(_prefabVidaBar, FindObjectOfType<Canvas>().transform).GetComponent<Image>();

        _barraVidaCheiaIMG = new List<Image>(_barraVidaIMG.GetComponentsInChildren<Image>()).Find(img => img != _barraVidaIMG);

        _rbAtirador = GetComponent<Rigidbody2D>();

        _atiradorAnimacao = GetComponent<Animator>();



        _cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        AtiradorSons = gameObject.transform.Find("Som").GetComponentsInChildren<AudioSource>();


        if (gameObject.CompareTag("AtiradorPistol")) {
            _armaDePatrulhaGO = transform.Find("ArmaPatrulha").gameObject;
        }
        if (gameObject.CompareTag("AtiradorShotgun")) {
            _armaDePatrulhaGO = transform.Find("ArmaPatrulha").gameObject;
        }

    }


    void Update() {
        InterfaceInimigo();

    }


    void FixedUpdate() {
        MoverAtrasPlayer();
    }



    void InterfaceInimigo() {
        //Barra de vida posição
        _barraVidaIMG.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, _posYbarraVida, 0));
        _barraVidaCheiaIMG.fillAmount = VidaAtual / VidaTotal;
        if (VidaAtual <= 0) {
            MorteInimigo();
        }

    }

    void MoverAtrasPlayer() {
        var diferencaPosX = transform.position.x - _charPlayer.transform.position.x;
        var diferencaPosY = transform.position.y - _charPlayer.transform.position.y;
        var distanciaMinima = Mathf.Abs(diferencaPosX) > 5f && Mathf.Abs(diferencaPosY) > 5f;

        if (_iaAtirador.EstaVendoPlayer && distanciaMinima) {
            _rbAtirador.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.transform.position, VelocidadeMovimento * Time.fixedDeltaTime));
            _atiradorAnimacao.SetBool("estaAndando", true);

            if (!AtiradorSons[0].isPlaying) {
                AtiradorSons[0].PlayDelayed(0.1f);

            }

        }
        else {
            _atiradorAnimacao.SetBool("estaAndando", false);
            AtiradorSons[0].Stop();
        }
    }

    void RecebendoDano() {
        _atiradorAnimacao.SetTrigger("recebendoDano");
        AtiradorSons[2].Play();
    }
    


    void MorteInimigo() {

        AtiradorSons[3].Play();
        AtiradorSons[4].Play();
        _atiradorAnimacao.SetBool("estaMorrendo", true);
        _charPlayer.AdicionarAlmas(1);
        Destroy(_barraVidaIMG.gameObject);
        Destroy(_armaDePatrulhaGO);
        _cameraShake.Shake(0.1f, 0.1f);
        GetComponent<SpriteRenderer>().sortingOrder = 3;

        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        
        Destroy(gameObject.transform.Find("MiraAtirador").gameObject);
        Destroy(this);


    }
    

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Bala")) {
            VidaAtual -= _charPlayer.BalaPistolDano;
            RecebendoDano();
            Destroy(collision.gameObject);


        }

        if (collision.gameObject.CompareTag("BalaShotgun")) {
            VidaAtual -= _charPlayer.BalaShotgunDano;
            RecebendoDano();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("BalaSubmachine")) {
            VidaAtual -= _charPlayer.BalaSubMachineDano;
            RecebendoDano();
            Destroy(collision.gameObject);

        }

       
    }
}
