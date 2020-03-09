using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraPlayer : MonoBehaviour {

  
    private CharPlayer _charPlayer;

    private GameObject _pistolGO;
    private GameObject _shotgunGO;
    private GameObject _submachineGO;

    public Vector2 _direcao { get; private set; } 


    void Start() {

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _pistolGO = transform.Find("PistolAtributos").transform.Find("PistolPlayer").gameObject;
        _shotgunGO = transform.Find("ShotgunAtributos").transform.Find("ShotgunPlayer").gameObject;
        _submachineGO = transform.Find("SubmachineAtributos").transform.Find("SubmachinePlayer").gameObject;

    }



    void Update() {

        MiraSeguindoCursor();
        FlipArmas();
        
    }


    void MiraSeguindoCursor() {

        Vector3 posicaoMouse = Input.mousePosition;
        posicaoMouse = Camera.main.ScreenToWorldPoint(posicaoMouse);


        _direcao = new Vector2(posicaoMouse.x - transform.position.x, posicaoMouse.y - transform.position.y);

        transform.up = _direcao;
    }

    void FlipArmas() {
        // Flip de acordo com a posicao do mouse.
        if (_direcao.x < 0) {
           // _playerGO.GetComponent<SpriteRenderer>().flipX = true;
            _pistolGO.GetComponent<SpriteRenderer>().flipY = true;
            _shotgunGO.GetComponent<SpriteRenderer>().flipY = true;
            _submachineGO.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if(_direcao.x > 0) {
            // _playerGO.GetComponent<SpriteRenderer>().flipX = false;
            _pistolGO.GetComponent<SpriteRenderer>().flipY = false;
            _shotgunGO.GetComponent<SpriteRenderer>().flipY = false;
            _submachineGO.GetComponent<SpriteRenderer>().flipY = false;

        }

     
    }
}
