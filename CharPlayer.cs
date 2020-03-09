using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharPlayer : MonoBehaviour {




    private float _vidaTotal = 5;
    private float _cooldownPlayerDano = 2.0f;
    private float _cooldownTiroPistol = 0.1f;
    private float _cooldownCarregarPistolTime = 2f;
    private float _cooldownTiroShotgun = 0.9f;
    private float _cooldownCarregarShotgun = 2f;
    private float _cooldownTiroSubmachine = 0.12f;
    private float _cooldownCarregarSubmachine = 2f;

    private bool _estaRecebendoDanoPlayer;
    private bool _estaDisparandoPistol = false;
    private bool _estaDisparandoSubmachine = false;
    private bool _estaDisparandoShotgun = false;

    private GameObject _sombraPlayer;
    private GameObject _miraPlayerGO;

    private GameObject _pistolAtributosUI;
    private GameObject _pistolAtributosGO;
    private GameObject _pistolGO;

    private GameObject _shotgunAtributosUI;
    private GameObject _shotgunAtributosGO;
    private GameObject _shotgunGO;

    private GameObject _submachineAtributosUI;
    private GameObject _submachineAtributosGO;
    private GameObject _submachineGO;

    [SerializeField]
    private GameObject _balaPistolPrefab;
    [SerializeField]
    private GameObject _capsulaBalaPrefab;
    [SerializeField]
    private GameObject _balaShotgunPrefab;
    [SerializeField]
    private GameObject _balaSubMachinePrefab;

    private Image _vidaAtualIMG;

    private Text _moedasPlayerTXT;
    private Text _almasPlayerTXT;
    private Text _balasNoPentePistolTXT;
    private Text _balasPenteShotgunTXT;
    private Text _penteReservaShotgunTXT;
    private Text _balasPenteSubmachineTXT;
    private Text _penteReservaSubmachineTXT;



    private Animator _playerAnimacao;
    private Animator _estrelasAnimacao;
    private Animator _pistolAnimacao;
    private Animator _shotgunAnimacao;
    private Animator _submachineAnimacao;

    private AudioSource _disparoPistolSom;
    private AudioSource _carregandoPistolSom;
    private AudioSource _pistolSemBalasSom;
    private AudioSource _disparoShotgunSom;
    private AudioSource _disparoSubmachineSom;
    private AudioSource _pegandoArmaSom;
    private AudioSource _moedaSom;
    private AudioSource _pegandoCoracaoSom;
    private AudioSource _recebendoDanoSom;

    private Transform _balaPistolSpawn;
    private Transform _balaShotgunSpawnA;
    private Transform _balaShotgunSpawnB;
    private Transform _balaShotgunSpawnC;
    private Transform _balaShotgunSpawnD;
    private Transform _balaShotgunSpawnE;
    private Transform _balaSubMachineSpawn;

    private Rigidbody2D _PlayerRb;
    private BoxCollider2D _playerCollider;


    private CameraShake _cameraShake;

    private MiraPlayer _miraPlayer;

    private MovePlayer _movePlayer;

    public float VidaAtual { get; private set; } = 5;

    public int MoedasPlayer { get; private set; }
    public int AlmasPlayer { get; private set; }
    public int BalasNoPentePistol { get; private set; } = 15;
    public int TotalBalasNoPentePistol { get; private set; } = 15;

    public int BalaPistolDano { get; private set; } = 2;
    public int BalasPenteShotgun { get; private set; } = 0;
    public int TotalPenteShotgun { get; } = 7;
    public int PenteReservaShotgun { get; private set; } = 0;
    public int TotalPenteReservaShotgun { get; } = 200;
    public int BalaShotgunDano { get; } = 10;

    public int BalasPenteSubmachine { get; private set; } = 0;
    public int TotalBalasPenteSubmachine { get; } = 30;
    public int PenteReservaSubmachine { get; private set; } = 0;
    public int TotalPenteReservaSubmachine { get; } = 300;
    public int BalaSubMachineDano { get; private set; } = 5;


    public bool EventoAtivo { get; set; } = false;
    public bool EstaMorto { get; private set; }

    public bool EstaCarregandoPistol { get; set; } = false;
    public bool EstaCarregandoShotgun { get; set; } = false;
    public bool EstaCarregandoSubmachine { get; set; } = false;

    

    void Awake() {

        _PlayerRb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<BoxCollider2D>();

        //Player Atributos UI.
        _almasPlayerTXT = GameObject.Find("AlmasTXT").GetComponent<Text>();


        //Instanciando os SFX.
        _pegandoArmaSom = GameObject.Find("PegandoArmaSom").GetComponent<AudioSource>();
        _disparoPistolSom = GameObject.Find("disparoPistolSom").GetComponent<AudioSource>();
        _carregandoPistolSom = GameObject.Find("carregandoPistolSom").GetComponent<AudioSource>();
        _pistolSemBalasSom = GameObject.Find("pistolSemBalasSom").GetComponent<AudioSource>();
        _moedaSom = GameObject.Find("MoedaSom").GetComponent<AudioSource>();
        _pegandoCoracaoSom = GameObject.Find("PegandoCoracaoSom").GetComponent<AudioSource>();
        _recebendoDanoSom = GameObject.Find("PlayerRecebendoDano").GetComponent<AudioSource>();

        //Estetica Player.
        _estrelasAnimacao = GameObject.Find("StarsAnimation").GetComponent<Animator>();
        _sombraPlayer = GameObject.Find("SombraPlayer");
        _playerAnimacao = GetComponent<Animator>();

        //Atributos UI instanciados.
        _pistolAtributosUI = GameObject.Find("PistolAtributosUI");
        _balasNoPentePistolTXT = GameObject.Find("balasNoPentePistolTXT").GetComponent<Text>();

        _shotgunAtributosUI = GameObject.Find("ShotgunAtributosUI");
        _balasPenteShotgunTXT = GameObject.Find("balasNoPenteShotgunTXT").GetComponent<Text>();
        _penteReservaShotgunTXT = GameObject.Find("balasReservaShotgunTXT").GetComponent<Text>();

        _submachineAtributosUI = GameObject.Find("SubmachineAtributosUI");
        _balasPenteSubmachineTXT = GameObject.Find("balasNoPenteSubmachineTXT").GetComponent<Text>();
        _penteReservaSubmachineTXT = GameObject.Find("balasReservaSubmachineTXT").GetComponent<Text>();

        _vidaAtualIMG = GameObject.Find("BarraVidaPlayerAtual").GetComponent<Image>();
        _moedasPlayerTXT = GameObject.Find("CoinTXT").GetComponent<Text>();




        //Atributos Armas GO

        _pistolAtributosGO = GameObject.Find("PistolAtributos");
        _pistolGO = GameObject.Find("PistolPlayer");
        _disparoPistolSom = GameObject.Find("disparoPistolSom").GetComponent<AudioSource>();
        _pistolAnimacao = GameObject.Find("PistolPlayer").GetComponent<Animator>();

        _shotgunAtributosGO = GameObject.Find("ShotgunAtributos");
        _shotgunGO = GameObject.Find("ShotgunPlayer");
        _disparoShotgunSom = GameObject.Find("DisparoShotgun").GetComponent<AudioSource>();
        _shotgunAnimacao = GameObject.Find("ShotgunPlayer").GetComponent<Animator>();

        _submachineAtributosGO = GameObject.Find("SubmachineAtributos");
        _submachineGO = GameObject.Find("SubmachinePlayer");
        _disparoSubmachineSom = GameObject.Find("DisparoSubmachine").GetComponent<AudioSource>();
        _submachineAnimacao = GameObject.Find("SubmachinePlayer").GetComponent<Animator>();

        _miraPlayerGO = gameObject.transform.Find("miraPlayerGO").gameObject;


        //# Balas
        _balaPistolSpawn = GameObject.Find("SpawnBalaPistol").GetComponent<Transform>();

        _balaShotgunSpawnA = GameObject.Find("SpawnBalaShotGunA").GetComponent<Transform>();
        _balaShotgunSpawnB = GameObject.Find("SpawnBalaShotGunB").GetComponent<Transform>();
        _balaShotgunSpawnC = GameObject.Find("SpawnBalaShotGunC").GetComponent<Transform>();
        _balaShotgunSpawnD = GameObject.Find("SpawnBalaShotGunD").GetComponent<Transform>();
        _balaShotgunSpawnE = GameObject.Find("SpawnBalaShotGunE").GetComponent<Transform>();

        _balaSubMachineSpawn = GameObject.Find("SpawnBalaSubmachine").GetComponent<Transform>();

        _cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        _miraPlayer = GetComponent<MiraPlayer>();
        _movePlayer = GetComponent<MovePlayer>();

        //# Ativos/Desativos.
        _pistolAtributosUI.SetActive(true);
        _shotgunAtributosUI.SetActive(false);
        _submachineAtributosUI.SetActive(false);

        _pistolAtributosGO.SetActive(true);
        _shotgunAtributosGO.SetActive(false);
        _submachineAtributosGO.SetActive(false);
    }


    void Update() {

        PermissaoDeExecucao();
        Estetica();
        UIstatusPlayer();

    }



    void Estetica() {

        if (GetComponent<SpriteRenderer>().flipX == true) {
            _sombraPlayer.transform.position = new Vector3(transform.position.x, _sombraPlayer.transform.position.y, _sombraPlayer.transform.position.z);
        }

        if (_pistolGO.GetComponent<SpriteRenderer>().flipY == true) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = false;
        }


    }

    void UIstatusPlayer() {
        //Vida do player
        _vidaAtualIMG.fillAmount = VidaAtual / _vidaTotal;



        if (VidaAtual <= 0) {
            VidaAtual = 0;
            EstaMorto = true;
            _cameraShake.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y);
            MortePlayer();

            //Debug.Log("Game Over!");
        }

        if (PenteReservaShotgun > TotalPenteReservaShotgun) {
            PenteReservaShotgun = TotalPenteReservaShotgun;
        }
        if (PenteReservaSubmachine > TotalPenteReservaSubmachine) {
            PenteReservaSubmachine = TotalPenteReservaSubmachine;
        }

        _moedasPlayerTXT.text = MoedasPlayer.ToString();
        _almasPlayerTXT.text = AlmasPlayer.ToString();

        // Atributos UI Ativados/Desativados
        if (_pistolAtributosGO.activeSelf) {
            _shotgunAtributosUI.SetActive(false);
            _submachineAtributosUI.SetActive(false);
            _pistolAtributosUI.SetActive(true);
        }
        else if (_shotgunAtributosGO.activeSelf) {
            _pistolAtributosUI.SetActive(false);
            _submachineAtributosUI.SetActive(false);
            _shotgunAtributosUI.SetActive(true);
        }
        else if (_submachineAtributosGO.activeSelf) {
            _pistolAtributosUI.SetActive(false);
            _shotgunAtributosUI.SetActive(false);
            _submachineAtributosUI.SetActive(true);

        }




        // PISTOLA UI
        _balasNoPentePistolTXT.text = BalasNoPentePistol.ToString() + "/";


        _balasPenteShotgunTXT.text = BalasPenteShotgun.ToString();
        _penteReservaShotgunTXT.text = "/" + PenteReservaShotgun.ToString();

        _balasPenteSubmachineTXT.text = BalasPenteSubmachine.ToString();
        _penteReservaSubmachineTXT.text = "/" + PenteReservaSubmachine.ToString();


    }

    void DisparosEcondicoes() {
        // UI da arma estar ativa; Não pode realizar disparos sem CD; Precisa ter balas no Pente; Não pode estar carregando.
        var requisitosDisparoPistol = _pistolAtributosGO.activeSelf == true && _estaDisparandoPistol == false && BalasNoPentePistol > 0 && EstaCarregandoPistol == false && EstaMorto == false;
        var requisitosDisparoShotgun = _shotgunAtributosGO.activeSelf && _estaDisparandoShotgun == false && BalasPenteShotgun > 0 && EstaCarregandoShotgun == false && EstaMorto == false;
        var requisitosDisparoSubmachine = _submachineAtributosGO.activeSelf && _estaDisparandoSubmachine == false && BalasPenteSubmachine > 0 && EstaCarregandoSubmachine == false && EstaMorto == false;

        //Pistol Disparo
        if (Input.GetMouseButtonDown(0) && requisitosDisparoPistol) {
            _pistolAnimacao.SetTrigger("disparo");
            Instantiate(_balaPistolPrefab, _balaPistolSpawn.transform.position, _balaPistolSpawn.transform.rotation);
            Instantiate(_capsulaBalaPrefab, _balaPistolSpawn.transform.position, _balaPistolSpawn.transform.rotation);
            DisparoBala("Pistol", 1);
            _cameraShake.Shake(0.05f, 0.1f);
            _disparoPistolSom.Play();
            _estaDisparandoPistol = true;
            StartCoroutine("CooldownTiroPistol");

            //Carregando a Pistol
            if (BalasNoPentePistol <= 0 && _pistolAtributosGO.activeSelf && !EstaCarregandoPistol) {
                RecarregarArma("Pistol");
                EstaCarregandoPistol = true;

            }

        }
        else if (Input.GetMouseButtonDown(0) && _pistolAtributosGO.activeSelf && _estaDisparandoPistol == false && BalasNoPentePistol <= 0) {
            _pistolSemBalasSom.pitch = 1f;
            _pistolSemBalasSom.Play();
        }

        //Shotgun Disparo
        if (Input.GetMouseButtonDown(0) && requisitosDisparoShotgun) {
            _shotgunAnimacao.SetTrigger("disparo");
            Instantiate(_balaShotgunPrefab, _balaShotgunSpawnA.transform.position, _balaShotgunSpawnA.transform.rotation);
            Instantiate(_balaShotgunPrefab, _balaShotgunSpawnB.transform.position, _balaShotgunSpawnB.transform.rotation);
            Instantiate(_balaShotgunPrefab, _balaShotgunSpawnC.transform.position, _balaShotgunSpawnC.transform.rotation);
            Instantiate(_balaShotgunPrefab, _balaShotgunSpawnD.transform.position, _balaShotgunSpawnD.transform.rotation);
            Instantiate(_balaShotgunPrefab, _balaShotgunSpawnE.transform.position, _balaShotgunSpawnE.transform.rotation);
            //Capsula
            Instantiate(_capsulaBalaPrefab, _balaShotgunSpawnA.transform.position, _balaShotgunSpawnA.transform.rotation);

            DisparoBala("Shotgun", 1);
            _disparoShotgunSom.Play();
            _cameraShake.Shake(0.10f, 0.1f);
            _estaDisparandoShotgun = true;
            StartCoroutine("CooldownTiroShotgun");
            //Carregando a Shotgun
            if (BalasPenteShotgun <= 0 && PenteReservaShotgun > 0 && _shotgunAtributosGO.activeSelf && !EstaCarregandoShotgun) {
                RecarregarArma("Shotgun");
                EstaCarregandoShotgun = true;
            }

        }
        else if (Input.GetMouseButtonDown(0) && _shotgunAtributosGO.activeSelf && _estaDisparandoShotgun == false && BalasPenteShotgun <= 0 && PenteReservaShotgun <= 0) {
            _pistolSemBalasSom.GetComponent<AudioSource>().pitch = 0.72f;
            _pistolSemBalasSom.Play();
        }

        //SubMachine-GUN
        if (Input.GetMouseButton(0) && requisitosDisparoSubmachine) {
            _submachineAnimacao.SetTrigger("disparo");
            Instantiate(_balaSubMachinePrefab, _balaSubMachineSpawn.transform.position, _balaSubMachineSpawn.transform.rotation);
            Instantiate(_capsulaBalaPrefab, _balaSubMachineSpawn.transform.position, _balaSubMachineSpawn.transform.rotation);
            DisparoBala("Submachine", 1);
            _disparoSubmachineSom.Play();
            _cameraShake.Shake(0.05f, 0.1f);
            _estaDisparandoSubmachine = true;
            StartCoroutine("CooldownTiroSubmachine");

            //Carregando submachine.
            if (BalasPenteSubmachine <= 0 && PenteReservaSubmachine > 0 && _submachineAtributosGO.activeSelf && !EstaCarregandoSubmachine) {
                RecarregarArma("Submachine");
                EstaCarregandoSubmachine = true;
            }


        }
        else if (Input.GetMouseButton(0) && _submachineAtributosGO.activeSelf && _estaDisparandoSubmachine == false && BalasPenteSubmachine <= 0 && PenteReservaSubmachine <= 0) {
            _pistolSemBalasSom.GetComponent<AudioSource>().pitch = 1.28f;
            _pistolSemBalasSom.Play();
        }


    }




    IEnumerator CooldownTiroPistol() {
        yield return new WaitForSeconds(_cooldownTiroPistol);
        _estaDisparandoPistol = false;
    }

    IEnumerator CooldownCarregarPistol() {
        yield return new WaitForSeconds(_cooldownCarregarPistolTime);
        EstaCarregandoPistol = false;
        BalasNoPentePistol = TotalBalasNoPentePistol;

    }



    IEnumerator CooldownTiroShotgun() {
        yield return new WaitForSeconds(_cooldownTiroShotgun);
        _estaDisparandoShotgun = false;
    }

    IEnumerator CooldownCarregarShotgun() {
        yield return new WaitForSeconds(_cooldownCarregarShotgun);

        if (PenteReservaShotgun >= TotalPenteShotgun) {
            PenteReservaShotgun -= TotalPenteShotgun - BalasPenteShotgun;
            BalasPenteShotgun = TotalPenteShotgun;

        }
        else {
            //Senão ele pega tudo que tem e zera a reserva.
            PenteReservaShotgun -= TotalPenteShotgun - BalasPenteShotgun;
            BalasPenteShotgun += TotalPenteShotgun - BalasPenteShotgun;
            if (PenteReservaShotgun < 0) {
                PenteReservaShotgun = 0;
            }

        }
        EstaCarregandoShotgun = false;
    }



    IEnumerator CooldownTiroSubmachine() {
        yield return new WaitForSeconds(_cooldownTiroSubmachine);
        _estaDisparandoSubmachine = false;
    }

    IEnumerator CooldownCarregarSubmachine() {
        yield return new WaitForSeconds(_cooldownCarregarSubmachine);

        // Se tiver mais que o total de balas na reserva.
        if (PenteReservaSubmachine >= TotalBalasPenteSubmachine) {
            PenteReservaSubmachine -= TotalBalasPenteSubmachine - BalasPenteSubmachine;
            BalasPenteSubmachine = TotalBalasPenteSubmachine;
        }
        else {
            //Senão ele pega tudo que tem e zera a reserva.
            BalasPenteSubmachine += PenteReservaSubmachine;
            PenteReservaSubmachine = 0;
        }
        EstaCarregandoSubmachine = false;
    }

    IEnumerator CoolDownDanoNoPlayer() {
        yield return new WaitForSeconds(_cooldownPlayerDano);
        _estaRecebendoDanoPlayer = false;
    }

    public void RecarregarArma(string armaAcarregar) {
        _carregandoPistolSom.Play();

        switch (armaAcarregar) {
            case "Pistol":
                EstaCarregandoPistol = true;
                StartCoroutine("CooldownCarregarPistol");
                break;
            case "Shotgun":
                EstaCarregandoShotgun = true;
                StartCoroutine("CooldownCarregarShotgun");
                break;
            case "Submachine":
                EstaCarregandoSubmachine = true;
                StartCoroutine("CooldownCarregarSubmachine");
                break;
            default:
                Debug.Log("Essa arma não existe ou algo está escrito errado.");
                break;

        }
        
    }

    public void DisparoBala(string qualArma, int balasDisparadas) {

        switch (qualArma){

            case "Pistol":
                BalasNoPentePistol -= balasDisparadas;
                break;
            case "Shotgun":
                BalasPenteShotgun -= balasDisparadas;
                break;
            case "Submachine":
                BalasPenteSubmachine -= balasDisparadas;
                break;
            default:
                Debug.Log("Essa arma não existe ou algo está escrito errado.");
                break;

        }

        
    }


    // -- Dano quando causado de alguma fonte ao player.
    public void DanoInfigido(int dano, float potenciaShake) {
        if (!_estaRecebendoDanoPlayer) {
            VidaAtual -= dano;
            _cameraShake.Shake(0.08f, potenciaShake);
            _playerAnimacao.SetTrigger("Dano");
            _recebendoDanoSom.Play();
            StartCoroutine("CoolDownDanoNoPlayer");
            _estaRecebendoDanoPlayer = true;
        }
    }



  


    public void AdicionarMoedas(int qtdMoedas) {
        MoedasPlayer += qtdMoedas;
    }

    public void AdicionarAlmas(int qtdAlmas) {
        AlmasPlayer += qtdAlmas;
    }

    public void RemoverAlmas(int qtdAlmas) {
        AlmasPlayer -= qtdAlmas;
    }


    // -- Quando pegar balas de alguma fonte.
    public void PegarBalasShotgun(int qntBalasShotgun) {
        PenteReservaShotgun += qntBalasShotgun;
        _pegandoArmaSom.Play();
    }


    public void PegarBalasSubmachine(int qntBalasSubmachine) {
        PenteReservaSubmachine += qntBalasSubmachine;
        _pegandoArmaSom.Play();
    }


    void MortePlayer() {

        if (EstaMorto) {
            _playerAnimacao.SetBool("estaMorto", true);
            Destroy(_miraPlayerGO);
            Destroy(_PlayerRb);
            Destroy(GetComponent<BoxCollider2D>());

        }


    }




    public void PermissaoDeExecucao() {
        if (!EventoAtivo) {
            ComandosPlayer();
            DisparosEcondicoes();

        }


    }


    void ComandosPlayer() {

        //Comandos Básicos mudança de arma
        if (Input.GetKeyDown(KeyCode.Alpha3) && _submachineAtributosGO.activeSelf == false) {
            _submachineAtributosGO.SetActive(true);
            _shotgunAtributosGO.SetActive(false);
            _pistolAtributosGO.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _shotgunAtributosGO.activeSelf == false) {
            _shotgunAtributosGO.SetActive(true);
            _submachineAtributosGO.SetActive(false);
            _pistolAtributosGO.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && _pistolAtributosGO.activeSelf == false) {
            _pistolAtributosGO.SetActive(true);
            _submachineAtributosGO.SetActive(false);
            _shotgunAtributosGO.SetActive(false);
        }

        //Carregando Pistol após apertar R.
        if (Input.GetKeyDown(KeyCode.R) && _pistolAtributosGO.activeSelf && BalasNoPentePistol < TotalBalasNoPentePistol && !EstaCarregandoPistol) {
            RecarregarArma("Pistol");
            EstaCarregandoPistol = true;
        }

        //Carregando Shotgun após apertar R.
        if (Input.GetKeyDown(KeyCode.R) && _shotgunAtributosGO.activeSelf && BalasPenteShotgun < TotalPenteShotgun && PenteReservaShotgun > 0 && !EstaCarregandoShotgun) {
            RecarregarArma("Shotgun");
            EstaCarregandoShotgun = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && _submachineAtributosGO.activeSelf && BalasPenteSubmachine < TotalBalasPenteSubmachine && PenteReservaSubmachine > 0 && EstaCarregandoSubmachine) {
            RecarregarArma("Submachine");
            EstaCarregandoSubmachine = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !EventoAtivo) {
            EventoAtivo = true;
            Time.timeScale = 0f;
            _pistolGO.GetComponent<SpriteRenderer>().enabled = false;
            _shotgunGO.GetComponent<SpriteRenderer>().enabled = false;
            _submachineGO.GetComponent<SpriteRenderer>().enabled = false;
        } else {
            EventoAtivo = false;
            Time.timeScale = 1f;
            _pistolGO.GetComponent<SpriteRenderer>().enabled = true;
            _shotgunGO.GetComponent<SpriteRenderer>().enabled = true;
            _submachineGO.GetComponent<SpriteRenderer>().enabled = true;
        }
    }





    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Moeda")) {
            _moedaSom.Play();
            _estrelasAnimacao.Play("");
            MoedasPlayer++;
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Coracao")) {
            _pegandoCoracaoSom.Play();
            _estrelasAnimacao.Play("");
            VidaAtual += 10;
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("ShotgunBalas")) {
            PegarBalasShotgun(35);
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("SubmachineBalas")) {
            PegarBalasSubmachine(90);
            Destroy(collision.gameObject);
        }


        // -- Controle Dano dos inimigos --


        if (collision.gameObject.name == "RangeDanoSlime") {
            DanoInfigido(2, 0.1f);
            collision.gameObject.transform.parent.GetComponent<SlimeComportamento>().AtacarPlayer();

        }
            if (collision.gameObject.CompareTag("BalaPistolEnemy")) {
            DanoInfigido(1, 0.1f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("BalaShotgunEnemy")) {
            DanoInfigido(1, 0.1f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Espinhos")) {
            DanoInfigido(1, 0.1f);
            _cameraShake.Shake(0.08f, 0.2f);

        }

        if (collision.gameObject.CompareTag("Bomben")) {
            DanoInfigido(1, 0.1f);

        }

        // -- Bola de fogo do Boss
        if (collision.gameObject.CompareTag("BolaDeFogo")) {
            DanoInfigido(3, 0.2f);

        }

        // -- Eventos do Cenário 

        if (collision.gameObject.CompareTag("Queda")) {
            Debug.Log("Você caiu!");
        }




    }




    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.name == "RangeDanoSlime") {
            DanoInfigido(2, 0.1f);
            collision.gameObject.transform.parent.GetComponent<SlimeComportamento>().AtacarPlayer();
        }

        if (collision.gameObject.CompareTag("Espinhos")) {
            DanoInfigido(5, 0.1f);
        }


    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "RangeDanoSlime") {
            collision.gameObject.transform.parent.GetComponent<SlimeComportamento>().PararDeAtacar();
        }
    }



}
