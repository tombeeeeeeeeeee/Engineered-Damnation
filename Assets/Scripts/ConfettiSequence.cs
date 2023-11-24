using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSequence : SequenceObject
{
    private bool hasBegun = false;

    // Update is called once per frame
    protected override void Update()
    {
        gameObject.SetActive(hasBegun);
        base.Update();
    }

    public override void Begin(bool decision)
    {
        hasBegun = !inSequence;
        base.Begin(decision);
    }
}
