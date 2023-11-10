using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class SymbolStampController : Focusable
{
    [SerializeField] SymbolRing innerRing;
    [SerializeField] SymbolRing outerRing;
    [SerializeField] Transform raycastPos;
    [SerializeField] GameObject plane1;
    [SerializeField] GameObject plane2;
    int currentRing = 0; // 0=outer 1=inner

    //   cycle input : turn left and right
    //    exit input : exit
    // action1 input : switch ring
    // action2 input : stamp

    private void Start()
    {
        plane1.GetComponent<MeshRenderer>().material = outerRing.symbol;
        plane2.GetComponent<MeshRenderer>().material = innerRing.symbol;
    }

    public override void NextPage()
    {
        if (currentRing == 0)
        {
            outerRing.TurnDial(1);
            plane1.GetComponent<MeshRenderer>().material = outerRing.symbol;
        }
        else
        {
            innerRing.TurnDial(1);
            plane2.GetComponent<MeshRenderer>().material = innerRing.symbol;
        }
    }

    public override void PreviousPage()
    {
        if (currentRing == 0)
        {
            outerRing.TurnDial(-1);
            plane1.GetComponent<MeshRenderer>().material = outerRing.symbol;
        }
        else
        {
            innerRing.TurnDial(-1);
            plane2.GetComponent<MeshRenderer>().material = innerRing.symbol;
        }
    }

    public override void Action1(InputAction.CallbackContext context)
    {
        // switch rings
        if (currentRing == 0)
            currentRing = 1;
        else
            currentRing = 0;
    }

    public override void Action2(InputAction.CallbackContext context)
    {
        // stamp
        PressStamp();
        Exit();
    }

    public void PressStamp()
    {
        SlabManager slab = null;

        Debug.Log(gameObject.name);

        RaycastHit hit;

        if(Physics.Raycast(raycastPos.position, -transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.GetComponentInChildren<SlabManager>())
                slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
        }

        if (slab != null)
        {
            slab.ChangeInner(innerRing.symbol, (uint)innerRing.symbolIndex);

            slab.ChangeOuter(outerRing.symbol, (uint)outerRing.symbolIndex);

            //Give a faint imprint of the press onto the slab
            //slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
