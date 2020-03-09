using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacionar : MonoBehaviour {

    public float velocidadeRotacao = -300f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate( transform.rotation.x, transform.rotation.y, velocidadeRotacao * Time.deltaTime, Space.Self);
    }

}
