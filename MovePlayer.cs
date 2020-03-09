using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {


    private float _velocidadeMovimento = 5f;
    private bool _estaCorrendo;


    private GameObject _playerGO;

    private Rigidbody2D _rbPlayer;

    private BoxCollider2D _collider2dPlayer;

    private Vector2 _movimentoPlayer;
    
    private Animator _playerAnim;
    
    private AudioSource _playerCorrendoSom;

    private CharPlayer _charPlayer;

    public bool EstaImovel { get; set; } = false;

    
    void Start() {
        _collider2dPlayer = GetComponent<BoxCollider2D>();
        _playerAnim = GetComponent<Animator>();
        _rbPlayer = GetComponent<Rigidbody2D>();
        _playerGO = gameObject;
        _playerCorrendoSom = GameObject.Find("PlayerCorrendo").GetComponent<AudioSource>();
        _charPlayer = GetComponent<CharPlayer>();


    }


    void Update() {
        if (_charPlayer.EventoAtivo == false) {
            Movimentacao();
            _velocidadeMovimento = 5f;
        } else {
            _velocidadeMovimento = 0f;
        }

        Aparencia();
    }



    void FixedUpdate() {
        _rbPlayer.MovePosition(_rbPlayer.position + _movimentoPlayer * _velocidadeMovimento * Time.fixedDeltaTime);

    }

    void Aparencia() {

        //Caso a sprite do player flipar, modifica o box collider2d.
        if (GetComponent<SpriteRenderer>().flipX == true) { // 0,03178325 --- -0,2346883 --- Size: 0,3862936 -- 0,369113 || flipado: -0,065offset
            _collider2dPlayer.offset = new Vector2(0f, -0.2346883f);
        } else {
            _collider2dPlayer.offset = new Vector2(0f, -0.2346883f);
        }
        


    }

    void Movimentacao() {

        _movimentoPlayer.x = Input.GetAxisRaw("Horizontal");
        _movimentoPlayer.y = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && !EstaImovel) {
            _playerAnim.SetBool("estaCorrendo", true);
            if (!_playerCorrendoSom.isPlaying) {
                _playerCorrendoSom.PlayDelayed(0.1f);

            }
           
           
           

        }
        else {
            _playerAnim.SetBool("estaCorrendo", false);
            _estaCorrendo = false;
            _playerCorrendoSom.Stop();
        }

        
    }


    



   

}
