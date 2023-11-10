using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class SymbolStampController : Focusable
{
    [SerializeField] SymbolRing innerSymbol;
    [SerializeField] SymbolRing outerSymbol;
    [SerializeField] Transform raycastPos;
    int currentRing = 0; // 0=outer 1=inner

    //   cycle input : turn left and right
    //    exit input : exit
    // action1 input : switch ring
    // action2 input : stamp

    public override void NextPage()
    {
        if (currentRing == 0)
            outerSymbol.TurnDial(-1);
        else
            innerSymbol.TurnDial(1);

    }

    public override void PreviousPage()
    {
        if (currentRing == 0)
            outerSymbol.TurnDial(1);
        else
            innerSymbol.TurnDial(-1);
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


        RaycastHit hit;

        if(Physics.Raycast(raycastPos.position, -transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.GetComponentInChildren<SlabManager>())
                slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
        }

        if (slab != null)
        {
            Debug.Log("slab hit");


            slab.ChangeInner(innerSymbol.symbol, (uint)innerSymbol.symbolIndex);

            slab.ChangeOuter(outerSymbol.symbol, (uint)outerSymbol.symbolIndex);

            //Give a faint imprint of the press onto the slab
            //slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
        else Debug.Log("no slab");
    }
}
