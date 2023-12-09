using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerDecisionLight : LightFlickerOffSequence
{
    public GameObject objectToDestroy;
    [SerializeField] SequenceObject trueSequence;
    [SerializeField] SequenceObject falseSequence;
    [SerializeField] DemonSummoningSpot dSS;

    protected override void Update()
    {
        base.Update();
        if (numberOfFlickers == 3) Destroy(objectToDestroy);
        else if (numberOfFlickers == 1) nextInSequence.Begin(decision);
    }


    public override void End()
    {
        nextInSequence = dSS.demonToSummon != null ? trueSequence : falseSequence;
        base.End();
    }
}
