using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(LightFlickerOffSequence))]
public class LightFlickerOnSequence : SequenceObject
{
    public float minTmeBeforeFlicker = 0;
    public float maxTmeBeforeFlicker = 1;

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        lengthOfOperation = Random.Range(minTmeBeforeFlicker, maxTmeBeforeFlicker);
    }
}
