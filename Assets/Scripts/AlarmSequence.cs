using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSequence : SequenceObject
{
    [SerializeField] Light[] lightsToTurnOff;
    [SerializeField] float rotationSpeed = 200;

    private void Update()
    {
        if (inSequence)
            timeInOperation += Time.deltaTime;
        if (timeInOperation > lengthOfOperation && inSequence)
            End();

        //rotate light
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime, 0));
    }

    public override void Begin(bool decision)
    {
        this.decision = decision;
        if(!inSequence)
        {
            inSequence = true;
            gameObject.SetActive(true);
            timeInOperation = 0;

            foreach(Light light in lightsToTurnOff)
                light.gameObject.SetActive(false);
        }
    }

    public override void End()
    {
        inSequence = false;
        if(nextInSequence)
            nextInSequence.Begin(decision);
    }
}
