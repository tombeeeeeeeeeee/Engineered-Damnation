using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SymbolRing : MonoBehaviour
{
    public Material[] symbols;

    [HideInInspector]
    public float anglePerSymbol = 360f;
    public int symbolIndex = 0;

    private void Start()
    {
        anglePerSymbol = 360 / symbols.Length;
    }

    public Material symbol { get { return symbols[symbolIndex]; } }

    public void TurnDial(int direction)
    {
        transform.Rotate(Vector3.up, direction * anglePerSymbol);
        symbolIndex = (symbolIndex + direction % symbols.Length);
    }
}
