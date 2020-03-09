using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour {

    Animator barrilAnimacao;

    AudioSource barrilQuebrandoSom;

    public int dropItemRandom;
    public GameObject[] barrilDentro;


    // Start is called before the first frame update
    void Start() {
        barrilAnimacao = GetComponent<Animator>();
        barrilQuebrandoSom = GameObject.Find("BarrilQuebrandoSom").GetComponent<AudioSource>();
        dropItemRandom = Random.Range(0, 20);
    }

    // Update is called once per frame
    void Update() {
       //dropItemRandom = Random.Range(0, 20);
        
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bala") || collision.gameObject.CompareTag("BalaShotgun") || collision.gameObject.CompareTag("BalaSubmachine")) {
            collision.gameObject.GetComponent<Animator>().SetTrigger("obstaculo");
            Destroy(collision.gameObject);
            barrilQuebrandoSom.Play();
            barrilAnimacao.SetTrigger("quebrando");
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CapsuleCollider2D>());
            if(dropItemRandom > 0 && dropItemRandom < barrilDentro.Length){
                Instantiate(barrilDentro[dropItemRandom], transform.position, Quaternion.identity);
            }
            



        }
    }

}
