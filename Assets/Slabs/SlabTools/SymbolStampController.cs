using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SymbolStampController : Focusable
{
    [SerializeField] SymbolRing innerSymbol;
    [SerializeField] SymbolRing outerSymbol;

    public override void NextPage()
    {
        outerSymbol.TurnDial(1);
    }

    public override void PreviousPage()
    {
        outerSymbol.TurnDial(-1);
    }


    public void PressStamp()
    {
        SlabManager slab = null;


        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.GetComponentInChildren<SlabManager>())
                slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
        }

        if (slab != null)
        {
            slab.ChangeInner(innerSymbol.symbol, (uint)innerSymbol.symbolIndex);

            slab.ChangeOuter(outerSymbol.symbol, (uint)outerSymbol.symbolIndex);

            //Give a faint imprint of the press onto the slab
            slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
