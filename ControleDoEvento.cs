using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoEvento : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {
        EventosTutorial.atual.AcaoManeiraDoEventoTrigar += EventoDoCaminho;
    }

    private void EventoDoCaminho() {
        //Mover a ponte.
        
    }

}
