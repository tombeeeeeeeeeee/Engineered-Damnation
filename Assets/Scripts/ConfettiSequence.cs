using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSequence : SequenceObject
{
    private bool hasBegun = false;
    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(hasBegun);
        if(inSequence)
            timeInOperation += Time.deltaTime;
        if (timeInOperation > lengthOfOperation)
            End();
    }

    public override void Begin(bool decision)
    {
        this.decision = decision;

        if (!inSequence)
        {
            hasBegun = true;
            inSequence = true;
            gameObject.SetActive(true);
            timeInOperation = 0;
        }
    }

    public override void End()
    {
        inSequence = false;
        timeInOperation = 0;
        nextInSequence.Begin(decision);
    }
}
