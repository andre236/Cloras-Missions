using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16f,16f);


    private void Update() {

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

    }


}
