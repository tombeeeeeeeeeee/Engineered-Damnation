using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RingManager : MonoBehaviour
{
    // Start is called before the first frame update\
    public Material[] symbols;

    [HideInInspector]
    public float anglePerSymbol = 360f;
    public int symbolIndex = 0;

    void Start()
    {
        anglePerSymbol = 360 / symbols.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material symbol { get { return symbols[symbolIndex]; } }

    public void TurnDial(InputAction.CallbackContext context)
    {
        transform.Rotate(Vector3.up, context.ReadValue<int>() * anglePerSymbol);
        symbolIndex = (symbolIndex + context.ReadValue<int>() ) % symbols.Length;
    }
}
