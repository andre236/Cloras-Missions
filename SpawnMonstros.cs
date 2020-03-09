using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonstros : MonoBehaviour {

   
    private float _spawnMonstrosTime = 3;
    private bool _cdSpawnMonstroAtivo = false;
    private int _valorMonstro;
    [SerializeField]
    private bool _levelAttackLigado;
 

    [SerializeField]
    private GameObject[] _monstroSpawnArray;

    [SerializeField]
    private Vector2[] _posicoesAleatorias;

    private LevelAttack _levelAttack;

    // 1 = slime 2 = atirador pistol 3 = atirador shotgun

    void Start() {
        _levelAttack = GameObject.Find("CameraShake").GetComponent<LevelAttack>();
        StartCoroutine("TimeSpawnMonstro"); 
    }

    IEnumerator TimeSpawnMonstro() {
        yield return new WaitForSeconds(_spawnMonstrosTime);
        if (_levelAttackLigado && _levelAttack._roundAtivo) {
            transform.position = _posicoesAleatorias[Random.Range(0, 4)];
        }
        _valorMonstro = Random.Range(0, 2);
        Instantiate(_monstroSpawnArray[_valorMonstro], transform.position, Quaternion.identity);
        _cdSpawnMonstroAtivo = false;
        StartCoroutine("TimeSpawnMonstro");
    }


    
}
