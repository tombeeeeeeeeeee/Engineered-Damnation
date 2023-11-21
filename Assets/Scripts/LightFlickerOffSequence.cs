using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

public class LightFlickerOffSequence : SequenceObject
{
    private int numberOfFlickers;
    public float timeBetweenFlicker = 0.3f;
    private float timeSinceLastFlicker = 0;
    private Light lightToFlicker;

    private void Start()
    {
        lightToFlicker = GetComponent<Light>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence) 
        {
            //Forces sequence to end when we choose.
            lengthOfOperation += 1;
            timeSinceLastFlicker += Time.deltaTime;

            //If finished flickering
            if(numberOfFlickers <= 0 )  End(); 

            //flicker!
            else if(timeSinceLastFlicker > timeBetweenFlicker)
            {
                timeSinceLastFlicker = 0;
                lightToFlicker.enabled = !lightToFlicker.enabled;
                numberOfFlickers--;
                lengthOfOperation = 0;
            }
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        numberOfFlickers = Random.Range(2, 6);
        numberOfFlickers *= 2;
    }
}
