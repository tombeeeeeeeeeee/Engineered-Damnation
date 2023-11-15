using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSequence : SequenceObject
{
    public override void Begin(bool decision)
    {
        this.decision = decision;

        if (!inSequence)
        {

        }
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
