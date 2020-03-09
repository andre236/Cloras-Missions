using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float velocidadeBala = 30f;
    public Animator balaAnimacao;
  



    // Start is called before the first frame update
    void Start() {
    

        balaAnimacao = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
     
            transform.Translate(Vector3.right * Time.deltaTime * velocidadeBala);
            Destroy(this.gameObject, 1f);
        
        
        
    }
    
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Parede") || collision.gameObject.CompareTag("Obstaculo")) {
           
            Destroy(this.gameObject, 1f);
        }
    }
}
