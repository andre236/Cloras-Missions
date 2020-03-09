using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigos : MonoBehaviour {

    //Características dos inimigos em Geral.
    public float vidaTotal, vidaAtual;
    public float moveSpeed = 2.5f;

    public Rigidbody2D rbInimigo;
    public GameObject esqueletoPosMorte;
    public GameObject MiraAtirador;


    public GameObject BarPrefab;
    Image Bar;
    Image BarFilled;
    public float alturaBar = 0.5f;



    //Ativar o comportamento do inimigo de acordo com a visão dele com o player.

    public GameObject visaoInimigo;
    public GameObject RangeDanoInimigo; //Se for meele

    //Animacoes Inimigo
    public Animator inimigoAnimator;
    public GameObject inimigoPosMorte;

    //Sons Inimigo
    public AudioSource inimigoRecebendoDanoSom;

    public AudioSource AtiradorCorrendoSom;
    public bool AtiradorCorrendoON = false;
    public float cooldownSomAndandoInicial = 2f, cooldownSomAndandoAtual = 2f;

    public AudioSource inimigoMorrendoSom;

    //Player Variaveis
    CharPlayer _charPlayer;

    //CameraShake
    public CameraShake camerashake;

    // Start is called before the first frame update
    void Start() {
        vidaAtual = vidaTotal;

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();

        Bar = Instantiate(BarPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();

        BarFilled = new List<Image>(Bar.GetComponentsInChildren<Image>()).Find(img => img != Bar);

        rbInimigo = GetComponent<Rigidbody2D>();

        inimigoAnimator = GetComponent<Animator>();

        RangeDanoInimigo = null;

        camerashake = GameObject.Find("CameraShake").GetComponent<CameraShake>();

        if (this.gameObject.CompareTag("AtiradorPistol") || this.gameObject.CompareTag("AtiradorShotgun") || this.gameObject.CompareTag("AtiradorSubmachine")) {
            inimigoRecebendoDanoSom = GameObject.Find("AtiradorRecebendoDanoSom").GetComponent<AudioSource>();
            AtiradorCorrendoSom = GameObject.Find("AtiradorCorrendoSom").GetComponent<AudioSource>();
            inimigoMorrendoSom = GameObject.Find("AtiradorMorrendoSom").GetComponent<AudioSource>();
            esqueletoPosMorte = GameObject.Find("AtiradorPosMorte");
        }

    }

    // Update is called once per frame
    void Update() {
        infoInimigo();
    }



  

    void infoInimigo() {
        //Barra de vida posição
        Bar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, alturaBar, 0));
        BarFilled.fillAmount = vidaAtual / vidaTotal;

        //Se o inimigo morrer:
        if (vidaAtual <= 0 && this.gameObject.CompareTag("AtiradorPistol")) {
            inimigoMorrendoSom.Play();
            Destroy(Bar.gameObject);
            camerashake.Shake(0.1f, 0.1f);
            inimigoAnimator.SetBool("estaMorrendo", true);
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            esqueletoPosMorte.GetComponent<SpriteRenderer>().sortingOrder = 2;
            esqueletoPosMorte.SetActive(true);
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this);
            //Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Bala")) {
            //Vida - o dano da pistol guardada em CharPlayer;
            vidaAtual -= _charPlayer.BalaPistolDano;
            inimigoAnimator.SetTrigger("recebendoDano");
            inimigoRecebendoDanoSom.Play();
            Destroy(collision.gameObject);


        }

        if (collision.gameObject.CompareTag("BalaShotgun")) {
            //Vida - o dano da Shotgun guardada em CharPlayer;
            vidaAtual -= _charPlayer.BalaShotgunDano;
            inimigoAnimator.SetTrigger("recebendoDano");
            inimigoRecebendoDanoSom.Play();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("BalaSubmachine")) {
            //Vida - o dano da Submachine guardada em CharPlayer;
            vidaAtual -= _charPlayer.BalaSubMachineDano;
            inimigoAnimator.SetTrigger("recebendoDano");
            inimigoRecebendoDanoSom.Play();
            Destroy(collision.gameObject);

        }
    }
}
