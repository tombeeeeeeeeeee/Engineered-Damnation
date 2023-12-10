using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnableSequence : SequenceObject
{
    public override void Begin(bool decision)
    {
        base.Begin(decision);
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
