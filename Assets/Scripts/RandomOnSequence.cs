using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RandomOnSequence : SequenceObject
{
    public float minTmeBeforeEvent = 0;
    public float maxTmeBeforeEvent = 1;

    void Start()
    {
        Begin(true);
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        lengthOfOperation = Random.Range(minTmeBeforeEvent, maxTmeBeforeEvent);
    }
}
