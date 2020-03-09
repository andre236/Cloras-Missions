using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAatirador : MonoBehaviour {


    private float _distancia = 15f;
    private float _velocidadeMovimento = 2.5f;

    private bool _podeAtirar = true;


    private GameObject _playerGO;
    private GameObject _armaPatrulha;
    private GameObject _armaMirandoGO;
    [SerializeField]
    private GameObject _balaPistolPrefab;
    [SerializeField]
    private GameObject _balaShotgunPrefab;
    [SerializeField]
    private GameObject _capsulaBala;

    private SpriteRenderer _armaSprite;

    [SerializeField]
    private LayerMask _layersPermitidas; //Obstaculo e Player
    
    private Transform _spawnBalaPistol;
    private Transform[] _spawnBalasShotgun;

    private Atirador _atirador;

    public bool EstaVendoPlayer { get; private set; } = false;


    void Start() {
        _atirador = GetComponent<Atirador>();
        _playerGO = GameObject.FindGameObjectWithTag("Player");


        _armaPatrulha = gameObject.transform.Find("ArmaPatrulha").gameObject;
        _armaSprite = gameObject.transform.Find("MiraAtirador").gameObject.transform.Find("Arma").GetComponent<SpriteRenderer>();
        _armaMirandoGO = gameObject.transform.Find("MiraAtirador").gameObject;

        if (gameObject.CompareTag("AtiradorPistol")) {
            _spawnBalaPistol = gameObject.transform.Find("MiraAtirador").gameObject.transform.Find("Arma").gameObject.transform.Find("SpawnBalaAtirador").GetComponent<Transform>();
        }
        else if (gameObject.CompareTag("AtiradorShotgun")) {
            _spawnBalasShotgun = gameObject.transform.Find("MiraAtirador").gameObject.transform.Find("Arma").gameObject.GetComponentsInChildren<Transform>();
        }


    }


    void Update() {

        if (EstaVendoPlayer == false) {
            _armaSprite.gameObject.SetActive(false);
            _armaPatrulha.SetActive(true);
        }

        Visao();
        Aparencia();
        Agressao();

        if (_atirador.VidaAtual <= 0) {
            Destroy(this);
        }

    }

    void FixedUpdate() {
        //    MoverAtrasPlayer();
    }

    void PerseguirPlayer() {
        var diferencaPosX = transform.position.x - _playerGO.transform.position.x;
        var diferencaPosY = transform.position.y - _playerGO.transform.position.y;
        var distanciaMinima = Mathf.Abs(diferencaPosX) > 5f && Mathf.Abs(diferencaPosY) > 5f;

        if (EstaVendoPlayer && distanciaMinima) {
            _atirador.GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, _playerGO.transform.position, _velocidadeMovimento * Time.fixedDeltaTime));

        }
    }

    void Visao() {
        Vector2 direcao = new Vector2(_playerGO.transform.position.x - _armaMirandoGO.transform.position.x, _playerGO.transform.position.y - _armaMirandoGO.transform.position.y);
        _armaMirandoGO.transform.up = direcao;


        Vector2 direcaoRaycast = new Vector2(_playerGO.transform.position.x - transform.position.x, _playerGO.transform.position.y - transform.position.y);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direcaoRaycast, _distancia, _layersPermitidas);

        if (hitInfo.collider) {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player")) {
                EstaVendoPlayer = true;
            }

        }
        else {
            Debug.DrawLine(transform.position, direcaoRaycast * _distancia, Color.green);
        }



    }

    void Aparencia() {


        //Se o player estiver a direita do atirador irá Flipar o Atirador para direção
        if (_playerGO.transform.position.x > GetComponentInParent<Transform>().transform.position.x) {
            GetComponentInParent<SpriteRenderer>().flipX = false;
            _armaSprite.flipY = false;
        }
        else {
            GetComponentInParent<SpriteRenderer>().flipX = true;
            _armaSprite.flipY = true;

        }
        //Se o atirador estiver flipado, flipa a arma.
        if (GetComponentInParent<SpriteRenderer>().flipX == true) {
            _armaPatrulha.GetComponent<SpriteRenderer>().flipY = true;
            _armaPatrulha.transform.localPosition = new Vector2(-0.46f, 0.19f);
        }
        else {
            _armaPatrulha.GetComponent<SpriteRenderer>().flipY = false;
            _armaPatrulha.transform.localPosition = new Vector2(0.43f, 0.19f);
        }

        //Quando ver player.
        if (EstaVendoPlayer) {
            _armaPatrulha.gameObject.SetActive(false);
            _armaSprite.gameObject.SetActive(true);
        }
    }

    void Agressao() {
        //Caso o player seja visto pelo atirador:
        if (EstaVendoPlayer) {
            Vector2 direcaoRaycast = new Vector2(_playerGO.transform.position.x - transform.position.x, _playerGO.transform.position.y - transform.position.y);

            //Visão Para Ativar o comportamento do Atirador.
            RaycastHit2D hitInfoOfensivo = Physics2D.Raycast(transform.position, direcaoRaycast, _distancia, _layersPermitidas);

            if (hitInfoOfensivo.collider) {
                Debug.DrawLine(transform.position, hitInfoOfensivo.point, Color.red);


                if (hitInfoOfensivo.collider.CompareTag("Player") && gameObject.CompareTag("AtiradorPistol") && _podeAtirar) {
                    DisparoPistol();

                }

                /*E se estiver equipado com a Shotgun */
                if (hitInfoOfensivo.collider.CompareTag("Player") && gameObject.CompareTag("AtiradorShotgun") && _podeAtirar)  {
                  
                    DisparoShotgun();

                }


            }
            else {
                Debug.DrawLine(transform.position, direcaoRaycast * _distancia, Color.green);
            }


        }
    }

  

    void DisparoPistol() {
        _atirador.AtiradorSons[1].Play();
        Instantiate(_balaPistolPrefab, _spawnBalaPistol.transform.position, _spawnBalaPistol.transform.rotation);
        Instantiate(_capsulaBala, _spawnBalaPistol.transform.position, _spawnBalaPistol.transform.rotation);
        _podeAtirar = false;
        StartCoroutine("CooldownDisparoPistol");
    }


    void DisparoShotgun() {
        _atirador.AtiradorSons[1].Play();
        for (int i = 1; i < 4; i++) {
            Instantiate(_balaShotgunPrefab, _spawnBalasShotgun[i].transform.position, _spawnBalasShotgun[i].transform.rotation);
           
        }
        Instantiate(_capsulaBala, _spawnBalasShotgun[2].transform.position, _spawnBalasShotgun[2].transform.rotation);
        _podeAtirar = false;
        StartCoroutine("CooldownDisparoShotgun");

    }

    IEnumerator CooldownDisparoPistol() {
        yield return new WaitForSeconds(1f);
        _podeAtirar = true;
    }

    IEnumerator CooldownDisparoShotgun() {
        yield return new WaitForSeconds(2f);
        _podeAtirar = true;
    }



}
