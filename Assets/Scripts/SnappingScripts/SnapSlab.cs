using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSlab : SnappingGameObject
{
    public override void OnTriggerStay(Collider other)
    {
        //If the object is the expected type.
        if (SnapType(other.gameObject) && ExpectedObject == null)
            ExpectedObject = other.gameObject;
        base.OnTriggerStay(other);
    }

    /// <summary>
    /// Checks if a passed object has a slab manager script
    /// </summary>
    /// <param name="obj">Object ot check</param>
    /// <returns>true if the object has a slab manager</returns>
    public override bool SnapType(GameObject obj)
    {
        return obj.GetComponent<SlabManager>() != null;
    }
}
