﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelAttack : MonoBehaviour {

   
    private float _timeRound = 120f;


    private GameObject _UImenu;
    private Text _timeRoundTXT;
    private Text _roundTXT;

    private CharPlayer _charPlayer;

    public bool _roundAtivo { get; private set; }
    public int _round { get; private set; }


    void Start() {
        _UImenu = GameObject.Find("UImenu");
        _UImenu.SetActive(false);
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _roundAtivo = true;
      //  _roundTXT = GameObject.Find("Round").GetComponent<Text>();
        _timeRoundTXT = GameObject.Find("TimeRound").GetComponent<Text>();

    }

    
    void Update() {
        AtualizandoUI();
        Morte();
    }




    public void AtualizandoUI() {
        _timeRoundTXT.text = _timeRound.ToString("0");
       

        if (_roundAtivo) {
            _timeRound -= Time.deltaTime;
            if(_timeRound <= 0){
                _timeRound = 0;
                _roundAtivo = false;
            }
        }
        
    }
    

    public void AvancarRound() {
        _round++;
    }

    public void Morte() {
        if(_charPlayer.VidaAtual == 0) {
            StartCoroutine("TelaPosMorte");
        }
  
        
    }

    public void RecomecarRound() {
        SceneManager.LoadScene("LevelAttack");
    }

    public void MenuInicial() {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator TelaPosMorte() {
        yield return new WaitForSeconds(2f);
        _roundAtivo = false;
        _UImenu.SetActive(true);
    }
}
