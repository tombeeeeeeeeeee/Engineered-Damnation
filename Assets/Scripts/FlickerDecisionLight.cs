using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerDecisionLight : LightFlickerOffSequence
{
    [SerializeField] SequenceObject trueSequence;
    [SerializeField] SequenceObject falseSequence;

    public override void End()
    {
        nextInSequence = decision ? trueSequence : falseSequence;
        base.End();
    }
}
