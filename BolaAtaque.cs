using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaAtaque : MonoBehaviour {

    float _movSpeed = 10f;
    float _timeDestroy = 1f;
    float _frequencia = 2f;
    public bool _indoDireita = true;
    
  

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _movSpeed * Time.deltaTime);

        Destroy(gameObject, 6f);

    }


   


    

}
