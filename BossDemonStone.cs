using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDemonStone : MonoBehaviour {

    //HP do boss
    float _hpMax = 2000;
    float _hpAtual = 2000;
    public float positionXhp = 1f;
    public float positionYhp = 1f;

    // -- Dificuldade do Boss para controlar.
    int dificuldadeEstagio = 1; // Irá de 1 a 4.
    int _movimentoAtaqueBoss = 1;
    bool _podeAtacar = true;
    float _cooldownAtaqueTime = 3f;
    int _repeticoesAtaque = 1;



    
    

    Rigidbody2D _rbBoss;

    [SerializeField] GameObject _barHPprefab;
    Image _barHPMaxIMG;
    Image _barHPfillIMG;

    //Animacoes Boss
    Animator _animacaoBoss;

    //Spawns Ataques
    Transform _circuloDeFogoSpawn;
    Transform _FogoDancanteSpawn;
    Transform _ParedeDeFogoDir;
    Transform _ParedeDeFogoEsq;

    //AtaqueBoss
    [SerializeField] GameObject _ballAttackPrefab;

    //Player
    CharPlayer _charPlayer;


    void OnEnable() {
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _rbBoss = GetComponent<Rigidbody2D>();

        _circuloDeFogoSpawn = gameObject.transform.Find("CirculoDeFogoSpawn");
        _FogoDancanteSpawn = gameObject.transform.Find("FogoDancanteSpawn");
        _ParedeDeFogoDir = gameObject.transform.Find("ParedeDeFogoDir");
        _ParedeDeFogoEsq = gameObject.transform.Find("ParedeDeFogoEsquerda");


        _animacaoBoss = GetComponent<Animator>();

        _barHPMaxIMG = Instantiate(_barHPprefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        _barHPfillIMG = new List<Image>(_barHPMaxIMG.GetComponentsInChildren<Image>()).Find(img => img != _barHPMaxIMG);
    }


    // Update is called once per frame
    void Update() {
        movimentoAtaqueBoss();
        infoBoss();

    }


    void infoBoss() {
        _barHPMaxIMG.transform.position = new Vector2(_barHPMaxIMG.transform.position.x, _barHPMaxIMG.transform.position.y);
        _barHPfillIMG.fillAmount = _hpAtual / _hpMax;
    }

    void movimentoAtaqueBoss() {
        // Quando o Boss tiver 25% de HP ou menos.
        if (_hpAtual <= (_hpMax / 4)) {
            dificuldadeEstagio = 4;
        }
        //Quando o Boss tiver 50% de HP ou menos.
        if (_hpAtual <= (_hpMax / 2)) {
            dificuldadeEstagio = 3;
        }

        if (_hpAtual <= (_hpMax / 2) + (_hpMax / 4)) {
            dificuldadeEstagio = 2;
        }


        // -- Dificuldade Fácil
        if (dificuldadeEstagio == 1) {
            // -- Fogo Dançante
            if (_movimentoAtaqueBoss == 1 && _podeAtacar) {
                InvokeRepeating("tirosFogoDancante", 0.25f, 0.05f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Círculo de Fogo
            if (_movimentoAtaqueBoss == 2 && _podeAtacar) {
                InvokeRepeating("circuloDeFogo", 0.5f, 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;

            }
            // -- Parede de Fogo lado Direito
            if (_movimentoAtaqueBoss == 3 && _podeAtacar) {
                Invoke("paredesDeFogoDir", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Parede de Fogo lado Esquerdo
            if (_movimentoAtaqueBoss == 4 && _podeAtacar) {
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss = 1;
            }

            // Dificuldade Normal
        }
        else if (dificuldadeEstagio == 2) {


            // -- Fogo Dançante
            if (_movimentoAtaqueBoss == 1 && _podeAtacar) {
                InvokeRepeating("tirosFogoDancante", 0.25f, 0.05f);
                Invoke("paredesDeFogoDir", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Círculo de Fogo
            if (_movimentoAtaqueBoss == 2 && _podeAtacar) {
                InvokeRepeating("circuloDeFogo", 0.5f, 0.25f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;

            }
            // -- Parede de Fogo lado Direito
            if (_movimentoAtaqueBoss == 3 && _podeAtacar) {
                Invoke("paredesDeFogoDir", 0.5f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Parede de Fogo lado Esquerdo
            if (_movimentoAtaqueBoss == 4 && _podeAtacar) {
                Invoke("paredesDeFogoDir", 0.5f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss = 1;
            }


        }   // Dificuldade Média
        else if (dificuldadeEstagio == 3) {


            // -- Fogo Dançante
            if (_movimentoAtaqueBoss == 1 && _podeAtacar) {
                InvokeRepeating("tirosFogoDancante", 0.25f, 0.05f);
                InvokeRepeating("circuloDeFogo", 0.5f, 0.25f);
                Invoke("paredesDeFogoDir", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Círculo de Fogo
            if (_movimentoAtaqueBoss == 2 && _podeAtacar) {
                InvokeRepeating("circuloDeFogo", 0.5f, 0.25f);
                InvokeRepeating("tirosFogoDancante", 0.25f, 0.05f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;

            }
            // -- Parede de Fogo lado Direito
            if (_movimentoAtaqueBoss == 3 && _podeAtacar) {
                InvokeRepeating("circuloDeFogo", 0.5f, 0.25f);
                Invoke("paredesDeFogoDir", 0.5f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss++;
            }

            // -- Parede de Fogo lado Esquerdo
            if (_movimentoAtaqueBoss == 4 && _podeAtacar) {
                InvokeRepeating("circuloDeFogo", 0.5f, 0.25f);
                Invoke("paredesDeFogoDir", 0.5f);
                Invoke("paredesDeFogoEsq", 0.5f);
                _podeAtacar = false;
                _movimentoAtaqueBoss = 1;
            }
        }


    }

    IEnumerator CooldownAtaqueBoss() {
        yield return new WaitForSeconds(_cooldownAtaqueTime);
        CancelInvoke("tirosFogoDancante");
        CancelInvoke("circuloDeFogo");
        _podeAtacar = true;
    }






    void tirosFogoDancante() {
        _animacaoBoss.SetBool("ataqueD", true);
        Instantiate(_ballAttackPrefab, _FogoDancanteSpawn.GetComponent<Transform>().transform.position, _FogoDancanteSpawn.GetComponent<Transform>().transform.rotation);

    }

    void circuloDeFogo() {
        _animacaoBoss.SetBool("ataqueD", false);
        _animacaoBoss.SetTrigger("ataqueC");
        Instantiate(_ballAttackPrefab, _circuloDeFogoSpawn.gameObject.transform.Find("A").transform);
        Instantiate(_ballAttackPrefab, _circuloDeFogoSpawn.gameObject.transform.Find("B").transform);
        Instantiate(_ballAttackPrefab, _circuloDeFogoSpawn.gameObject.transform.Find("C").transform);
        Instantiate(_ballAttackPrefab, _circuloDeFogoSpawn.gameObject.transform.Find("D").transform);
    }

    void paredesDeFogoDir() {
        //_ParedeDeFogoDir
        _animacaoBoss.SetBool("ataqueD", false);
        _animacaoBoss.SetTrigger("ataqueB");
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeA").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeB").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeC").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeD").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeE").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeF").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoDir.gameObject.transform.Find("ParedeG").transform);
    }

    void paredesDeFogoEsq() {
        //_ParedeDeFogoEsquerdo
        _animacaoBoss.SetBool("ataqueD", false);
        _animacaoBoss.SetTrigger("ataqueA");
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeA").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeB").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeC").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeD").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeE").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeF").transform);
        Instantiate(_ballAttackPrefab, _ParedeDeFogoEsq.gameObject.transform.Find("ParedeG").transform);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bala")) {
            Destroy(collision.gameObject);
            _animacaoBoss.SetTrigger("damage");
            _hpAtual -= _charPlayer.BalaPistolDano;
        }

        if (collision.gameObject.CompareTag("BalaShotgun")) {
            Destroy(collision.gameObject);
            _animacaoBoss.SetTrigger("damage");
            _hpAtual -= _charPlayer.BalaShotgunDano;
        }

        if (collision.gameObject.CompareTag("BalaSubmachine")) {
            Destroy(collision.gameObject);
            _animacaoBoss.SetTrigger("damage");
            _hpAtual -= _charPlayer.BalaSubMachineDano;
        }
    }

}
