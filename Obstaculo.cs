using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {


    private void OnEnable() {
        this.gameObject.layer = 8; // 8 == obstáculo
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Bala") || collision.gameObject.CompareTag("BalaShotgun") || collision.gameObject.CompareTag("BalaSubmachine")) {
            collision.gameObject.GetComponent<Bala>().velocidadeBala = 0;
            collision.gameObject.GetComponent<Animator>().SetTrigger("obstaculo");
            Destroy(collision.gameObject, 0.5f);
        }

        if(collision.gameObject.name== "FogoEnemyGO") {

        }
    }
}
