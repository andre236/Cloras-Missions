using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosTutorial : MonoBehaviour {

    public static EventosTutorial atual;

    void Awake() {
        atual = this;
    }

    public event Action AcaoManeiraDoEventoTrigar;

    public void ManeiraDoEventoTrigar() {

        AcaoManeiraDoEventoTrigar?.Invoke();
    }

}
