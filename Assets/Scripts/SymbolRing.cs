using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SymbolRing : MonoBehaviour
{
    public Material[] symbols;

    [HideInInspector]
    public float anglePerSymbol = 360f;
    public int symbolIndex = 1;

    private void Start()
    {
        anglePerSymbol = 360 / symbols.Length;
    }

    public Material symbol { get { return symbols[symbolIndex - 1]; } }

    public void TurnDial(int direction)
    {
        direction = Math.Sign(direction); // argument should be either -1 or 1, but just to make sure

        transform.Rotate(Vector3.up, direction * anglePerSymbol);
        symbolIndex -= direction;
        //symbolIndex = (symbolIndex + direction % symbols.Length);


        if (symbolIndex > symbols.Length)
            symbolIndex = 1;
        else if (symbolIndex < 1)
            symbolIndex = symbols.Length;

        //Debug.Log("symbol index: " + symbolIndex + " of " + symbols.Length);
    }
}
