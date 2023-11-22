using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerDecisionLight : LightFlickerOffSequence
{
    public GameObject objectToDestroy;
    [SerializeField] SequenceObject trueSequence;
    [SerializeField] SequenceObject falseSequence;

    protected override void Update()
    {
        base.Update();
        if (numberOfFlickers == 1) Destroy(objectToDestroy);
    }


    public override void End()
    {
        nextInSequence = decision ? trueSequence : falseSequence;
        base.End();
    }
}
