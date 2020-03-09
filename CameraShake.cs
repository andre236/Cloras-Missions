using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Camera _cameraPrincipal;
    private Transform _cameraSecundaria;
    private Transform _posicaoPlayer;
    private Transform _cameraComPlayerMorto;
    private CharPlayer _charPlayer;

    public float ShakeAmount { get; set; } = 0; // A magnitude
    public float LengthTime { get; set; } = 0.01f; // Tempo de "tremedeira".



    void Awake() {

        if (_cameraPrincipal == null) {
            _cameraPrincipal = Camera.main;
        }

        _cameraSecundaria = GetComponent<Transform>();
        _posicaoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _charPlayer = GameObject.Find("Player").GetComponent<CharPlayer>();
        _cameraComPlayerMorto = GameObject.FindGameObjectWithTag("Player").transform.Find("PosicaoCameraPlayerMorto").GetComponent<Transform>();
    }

    

    private void FixedUpdate() {
        Vector3 posicaoMouse = Input.mousePosition;
        posicaoMouse = Camera.main.ScreenToWorldPoint(posicaoMouse);

        if (!_charPlayer.EstaMorto) {
            transform.position = Vector2.MoveTowards(_posicaoPlayer.position, posicaoMouse, 2f);

        } else {
            transform.position = _cameraComPlayerMorto.position;
        }


    }


    public void Shake(float amt, float length) {
        ShakeAmount = amt;
        LengthTime = length;
        //InvokeRepeating é o método para conjurar Um método específico repetidas vezes e por um tempo determinado.
        /*Ex: InvokeRepeating("NOME_DO_SEU_METODO"), "TEMPO(EM FLOAT)", "QUANTIDADE DE VEZES");
         * já o método Invoke é o mesmo que InvokeRepeating porém sem determinar quantas vezes.
         */
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void DoShake() {

        //Quantidade de "tremer" for maior que 0.
        if (ShakeAmount > 0) {

            /* 1 - camPos receberá a posição da câmera
             * 2 - É gerado 1 valor aleatório entre 0.0 e 0.1 * quantidade de Tremer(shakeAmount) * 2 - a mesma quantidade de Tremer. Esse será o valor X
               3 - Faço isso novamente para o valor Y.
               4 - atribuo os valores gerados de X e Y SEPARADAMENTE para a camPos.
               5 - atribuo para posição da camera principal (mainCam) a camPos.
             */

            Vector3 camPos = _cameraPrincipal.transform.position;

            float offsetX = Random.value * ShakeAmount * 2 - ShakeAmount;
            float offsetY = Random.value * ShakeAmount * 2 - ShakeAmount;
            float offsetZ = -10f;
            camPos.x += offsetX;
            camPos.y += offsetY;
            camPos.z = offsetZ;

            _cameraPrincipal.transform.position = camPos;
        }
    }

    void StopShake() {
        CancelInvoke("DoShake");
        float offsetZ = -10f;
        Vector3 filtroVector = new Vector3(_cameraSecundaria.transform.position.x, _cameraSecundaria.transform.position.y, offsetZ);
        _cameraSecundaria.position = filtroVector;

        _cameraPrincipal.transform.localPosition = new Vector3(0, 0, offsetZ);


    }


   
}
