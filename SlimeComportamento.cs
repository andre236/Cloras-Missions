
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeComportamento : MonoBehaviour {


    private float _vidaTotal = 6;
    private float _distancia = 15f;
    private float _alturaBarraVida = 0.5f;
    private float _velocidadeMovimento = 2.5f;

    private GameObject _rangeAtaqueGO;
    private GameObject _slimePosMorteGO;
    [SerializeField]
    private GameObject _barraVidaPrefab;
    

    private Rigidbody2D _rbSlime;

    private Image _barraVida;
    private Image _barraVidaPreenchida;

    private AudioSource[] _slimeAudio; //0 = andando; 1 = Recebendo Dano; 2 = Slime Morrendo; 3 = Morte Genérica.

    private Animator _slimeAnimacao;

    [SerializeField]
    private LayerMask _layersPermitidas;

    private CharPlayer _charPlayer;
    private CameraShake _cameraShake;


    public float VidaAtual { get; private set; } = 6;

    public bool ViuPlayer { get; private set; }
    public bool EstaAndando { get; private set; }

    void Awake() {

        _rangeAtaqueGO = transform.Find("RangeDanoSlime").gameObject;
        _slimePosMorteGO = transform.Find("PosMorte").gameObject;

        _barraVida = Instantiate(_barraVidaPrefab, FindObjectOfType<Canvas>().transform.Find("barrasDeVida").transform).GetComponent<Image>();
        _barraVidaPreenchida = new List<Image>(_barraVida.GetComponentsInChildren<Image>()).Find(img => img != _barraVida);
        

        _rbSlime = GetComponent<Rigidbody2D>();

        _slimeAudio = transform.Find("SlimeSounds").GetComponentsInChildren<AudioSource>();
        _slimeAnimacao = GetComponent<Animator>();

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();


        VidaAtual = _vidaTotal;
    }

    void Start() {
        _slimePosMorteGO.SetActive(false);
    }

    void Update() {
        Comportamento();
        InterfaceSlime();


    }

    private void FixedUpdate() {
        Movimentacao();

    }

    void InterfaceSlime() {
        //Barra de vida posição
        _barraVida.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, _alturaBarraVida, 0));
        _barraVidaPreenchida.fillAmount = VidaAtual / _vidaTotal;
    }

    void Comportamento() {

        Vector2 direcao = new Vector2(_charPlayer.transform.position.x - transform.position.x, _charPlayer.transform.position.y - transform.position.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direcao, _distancia, _layersPermitidas);

        if (hitInfo.collider) {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.CompareTag("Player")) {
                ViuPlayer = true;
            }

        }


        if (ViuPlayer) {
            PerseguirPlayer();

            if (direcao.x < 0) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else {
                GetComponent<SpriteRenderer>().flipX = false;
            }

        }

      



    }

    void Movimentacao() {
        if (EstaAndando) {
            _slimeAnimacao.SetBool("estaAndando", true);
            _rbSlime.MovePosition(Vector2.MoveTowards(transform.position, _charPlayer.transform.position, _velocidadeMovimento * Time.fixedDeltaTime));

            if (EstaAndando && !_slimeAudio[0].isPlaying) {
                _slimeAudio[0].Play();

            }
        }
        else {
            EstaAndando = false;
            _slimeAnimacao.SetBool("estaAndando", false);
            _rbSlime.transform.position = new Vector2 (transform.position.x, transform.position.y);
            
        }



    }

    public void PerseguirPlayer() {
        EstaAndando = true;
        _slimeAnimacao.SetBool("estaAndando", true);
    }

    public void AtacarPlayer() {
        _slimeAnimacao.SetBool("estaAtacando", true);
        
    }

    public void PararDeAtacar() {
        _slimeAnimacao.SetBool("estaAtacando", false);
    }

    public void RecebendoDano(int danoInfligido) {
        VidaAtual -= danoInfligido;
        _slimeAnimacao.SetTrigger("recebendoDano");
        _slimeAudio[1].Play();

        if (VidaAtual <= 0) {
            MorteSlime();
        }
    }


    public void MorteSlime() {
        _slimeAudio[0].Stop();
        _slimeAudio[3].Play();
        _slimeAudio[2].Play();

        _charPlayer.AdicionarAlmas(1);
        _cameraShake.Shake(0.1f, 0.1f);
        _slimeAnimacao.SetBool("estaMorrendo", true);

        GetComponent<SpriteRenderer>().sortingOrder = 3;

        Destroy(_rangeAtaqueGO);
        Destroy(_barraVida.gameObject);

        _slimePosMorteGO.GetComponent<SpriteRenderer>().sortingOrder = 2;
        _slimePosMorteGO.SetActive(true);

        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(this);
    }


    

    private void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.gameObject.CompareTag("Bala")) {
            ViuPlayer = true;
            RecebendoDano(_charPlayer.BalaPistolDano);
            Destroy(collision.gameObject);


        }

        if (collision.gameObject.CompareTag("BalaShotgun")) {
            ViuPlayer = true;
            RecebendoDano(_charPlayer.BalaShotgunDano);
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("BalaSubmachine")) {
            ViuPlayer = true;
            RecebendoDano(_charPlayer.BalaSubMachineDano);
            Destroy(collision.gameObject);

        }
    }

}
