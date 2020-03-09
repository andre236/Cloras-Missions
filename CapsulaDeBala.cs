using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulaDeBala : MonoBehaviour {

    private Rigidbody2D _rbCapsula;

    private float _rotacaoZ = 2f;
    [SerializeField]
    private float _forcaY = 10f;
    [SerializeField]
    private float _forcaX = 4f;

    private SpriteRenderer _charPlayerSprite;

    // Start is called before the first frame update
    void Start() {
        _rbCapsula = GetComponent<Rigidbody2D>();

        _charPlayerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

        _rbCapsula.AddForce(new Vector2(Random.Range(-1f, 1f) * _forcaX, 1f * _forcaY));

    }



    // Update is called once per frame
    void Update() {

        _rotacaoZ += _rotacaoZ + 2f * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, _rotacaoZ);
            
        Destroy(gameObject, 1f);
    }


}
