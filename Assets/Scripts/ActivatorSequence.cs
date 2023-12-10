using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorSequence : SequenceObject
{
    [SerializeField] GameObject[] turnOnObjects;
    [SerializeField] GameObject[] turnOffObjects;

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            foreach (GameObject go in turnOnObjects) go.SetActive(true);
            foreach (GameObject go in turnOffObjects) go.SetActive(false);
        }
    }
}
