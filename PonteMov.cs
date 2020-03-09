using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteMov : MonoBehaviour {

    [SerializeField]
    GameObject _ponte;
    [SerializeField]
    Collider2D _quedaCollA;
    [SerializeField]
    Vector2 _offsetPonte, _offsetCollA;
    

    public void MovimentarPonte() {
        _ponte.transform.localPosition = new Vector2(_offsetPonte.x, _offsetPonte.y);
        _quedaCollA.offset = new Vector2(_offsetCollA.x, _offsetCollA.y);

    }
}
