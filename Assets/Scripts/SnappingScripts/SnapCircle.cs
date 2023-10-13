using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCircle:SnappingGameObject
{
    public override void OnTriggerEnter(Collider other)
    {
        ExpectedObject = other.GetComponent<CircleStencil>() != null ? other.gameObject : null;

        base.OnTriggerEnter(other);

        if (GetComponentInParent<SymbolStampController>())
            GetComponentInParent<SymbolStampController>().CircleIndex = other.GetComponent<CircleStencil>().CircleIndex;
    }
    public override void Update()
    {
        base.Update();
        if (!snapped)
            GetComponentInParent<SymbolStampController>().CircleIndex = 0;
    }
}  
