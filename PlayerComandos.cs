using UnityEngine;

public class PlayerComandos : MonoBehaviour {

    private GameObject _pistolAtributosGO,
                       _shotgunAtributosGO,
                       _submachineAtributosGO;

    private CharPlayer _charPlayer;

    // Start is called before the first frame update
    void Awake() {

        _charPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CharPlayer>();
        _pistolAtributosGO = GameObject.Find("miraPlayerGO").transform.GetChild(0).gameObject;
        _shotgunAtributosGO = GameObject.Find("ShotgunAtributos").transform.GetChild(1).gameObject;
        _submachineAtributosGO = GameObject.Find("SubmachineAtributos").transform.GetChild(2).gameObject;



    }

    void Update() {
        ComandosPlayer();
        
    }


    void ComandosPlayer() {

        //Comandos Básicos mudança de arma
        if (Input.GetKeyDown(KeyCode.Alpha3) && _submachineAtributosGO.activeSelf == false) {
            _submachineAtributosGO.SetActive(true);
            _shotgunAtributosGO.SetActive(false);
            _pistolAtributosGO.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _shotgunAtributosGO.activeSelf == false) {
            _shotgunAtributosGO.SetActive(true);
            _submachineAtributosGO.SetActive(false);
            _pistolAtributosGO.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && _pistolAtributosGO.activeSelf == false) {
            _pistolAtributosGO.SetActive(true);
            _submachineAtributosGO.SetActive(false);
            _shotgunAtributosGO.SetActive(false);
        }

        //Carregando Pistol após apertar R.
        if (Input.GetKeyDown(KeyCode.R) && _pistolAtributosGO.activeSelf && _charPlayer.BalasNoPentePistol < _charPlayer.TotalBalasNoPentePistol && !_charPlayer.EstaCarregandoPistol) {
            _charPlayer.RecarregarArma("Pistol");
            _charPlayer.EstaCarregandoPistol = true;
        }

        //Carregando Shotgun após apertar R.
        if (Input.GetKeyDown(KeyCode.R) && _shotgunAtributosGO.activeSelf && _charPlayer.BalasPenteShotgun < _charPlayer.TotalPenteShotgun && _charPlayer.PenteReservaShotgun > 0 && !_charPlayer.EstaCarregandoShotgun) {
            _charPlayer.RecarregarArma("Shotgun");
            _charPlayer.EstaCarregandoShotgun = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && _submachineAtributosGO.activeSelf && _charPlayer.BalasPenteSubmachine < _charPlayer.TotalBalasPenteSubmachine && _charPlayer.PenteReservaSubmachine > 0 && _charPlayer.EstaCarregandoSubmachine) {
            _charPlayer.RecarregarArma("Submachine");
            _charPlayer.EstaCarregandoSubmachine = true;
        }


    }


}
