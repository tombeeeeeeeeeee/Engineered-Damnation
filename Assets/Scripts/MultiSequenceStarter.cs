using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSequenceStarter : SequenceObject
{
    [SerializeField] SequenceObject[] sequencesToStart;

    public override void Begin(bool decision)
    {
        foreach(SequenceObject sequence in sequencesToStart) sequence.Begin(decision);
        base.Begin(decision);
    }
}
