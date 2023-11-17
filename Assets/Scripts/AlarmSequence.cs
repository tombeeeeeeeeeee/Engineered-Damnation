using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSequence : SequenceObject
{
    [SerializeField] Light[] lightsToTurnOff;
    [SerializeField] float rotationSpeed = 200;

    protected override void Update()
    {
        base.Update();

        //rotate light
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime, 0));
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(!inSequence)
        { 
            foreach(Light light in lightsToTurnOff)
                light.gameObject.SetActive(false);
        }
    }
}
