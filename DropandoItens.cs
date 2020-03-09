using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropandoItens : MonoBehaviour {

    public int direcaoAleatoria; // 1 a 8
    public float lancamento = 2f;
    public float redutor = 1f;
    public Rigidbody2D rbMoeda;
  

    

    // Start is called before the first frame update
    void Start() {
        // direcaoAleatoria = Random.Range(1, 8);
        direcaoAleatoria = 1;
        /* direcaoAleatoria = Random.Range(1, 8);
         switch (direcaoAleatoria) {
             case 1:

         }*/
        rbMoeda = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        lancamento -= Time.deltaTime * redutor;
        switch (direcaoAleatoria) {
            //Direita
            case 1:
              transform.Translate(Vector3.right * Time.deltaTime * lancamento, Space.World);
                break;
            case 2:
                transform.Translate(Vector3.left * Time.deltaTime * lancamento, Space.World);
                break;
            case 3:
                transform.Translate(Vector3.up * Time.deltaTime * lancamento, Space.World);
                break;
            case 4:
                transform.Translate(Vector3.down * Time.deltaTime * lancamento, Space.World);
                break;
            default:
                Debug.Log("ocorreu um erro?");
                break;
        }
        
        
        if(lancamento <= 0){
            lancamento = 0;
        }
        
    }

    
}
