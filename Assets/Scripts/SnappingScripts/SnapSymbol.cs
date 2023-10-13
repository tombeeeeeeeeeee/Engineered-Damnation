using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSymbol : SnappingGameObject
{
    public override void OnTriggerEnter(Collider other)
    {
        ExpectedObject = other.GetComponent<SymbolStencil>() != null ? other.gameObject : null;

        base.OnTriggerEnter(other);

        if(GetComponentInParent<SymbolStampController>())
            GetComponentInParent<SymbolStampController>().SymbolIndex = other.GetComponent<SymbolStencil>().SymbolIndex;
    }

    public override void Update()
    {
        base.Update();
        if (!snapped)
            GetComponentInParent<SymbolStampController>().SymbolIndex = 0;
    }
}
