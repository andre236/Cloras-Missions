using System.Collections;
using UnityEngine;

public class EspinhosChao : MonoBehaviour {


    Animator _espinhosChaoAnima;

    float _cooldownEspinhosAtual = 0.5f;

    float _espinhosAtivoTempoAtual = 1f;


    CharPlayer _charPlayer;

    void OnEnable() {
        _espinhosChaoAnima = GetComponent<Animator>();
        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        //Quando player pisar nos espinhos, irá entrar em cooldown.
        if (collision.gameObject.CompareTag("Player") && !_espinhosChaoAnima.GetBool("espinhosAtivo")) {
            StartCoroutine("CooldownEspinhosParaAtivar");
            
            
        } else if (collision.gameObject.CompareTag("Player") && _espinhosChaoAnima.GetBool("espinhosAtivo")) {
                _charPlayer.DanoInfigido(2, 0.1f);
            
        } else {
            _espinhosChaoAnima.SetBool("espinhosAtivo",false);
        }


    }

    private void OnTriggerStay2D(Collider2D collision) {
        //Se estiver colidindo com o player e a animação estiver ativa
        if (collision.gameObject.CompareTag("Player") && _espinhosChaoAnima.GetBool("espinhosAtivo")) {
                _charPlayer.DanoInfigido(2, 0.1f);
        }
        
    }

    IEnumerator CooldownEspinhosParaAtivar() {
        yield return new WaitForSeconds(_cooldownEspinhosAtual);
        _espinhosChaoAnima.SetBool("espinhosAtivo", true);
        StartCoroutine("CooldownEspinhosParaDesativar");

    }

    IEnumerator CooldownEspinhosParaDesativar(){
        yield return new WaitForSeconds(_espinhosAtivoTempoAtual);
        _espinhosChaoAnima.SetBool("espinhosAtivo", false);
    }
   

    


}
