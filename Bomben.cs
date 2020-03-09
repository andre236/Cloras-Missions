using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomben : MonoBehaviour {

    [SerializeField]
    bool _paraDireita;

    float velocidadeMov = 1f;
    Rigidbody2D _rbBomben;
    SpriteRenderer _SpriteBomben;

    // Start is called before the first frame update
    void Start() {
        _rbBomben = GetComponent<Rigidbody2D>();
        _SpriteBomben = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate() {
        if (_paraDireita) {
            _rbBomben.AddForce(Vector2.right);
        } else {
            _SpriteBomben.flipX = true;
            _rbBomben.AddForce(Vector2.left);
        }
        
        
    }

   
}
