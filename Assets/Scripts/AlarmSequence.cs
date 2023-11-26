using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSequence : SequenceObject
{
    [SerializeField] Light[] lightsToTurnOff;
    [SerializeField] float rotationSpeed = 200;
    [SerializeField] AudioClip alarmSound;

    protected override void Update()
    {
        base.Update();

        //rotate light
        transform.Rotate(new Vector3(0,rotationSpeed * Gameplay.deltaTime, 0));
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        { 
            foreach(Light light in lightsToTurnOff)
                Destroy(light);
            GetComponent<AudioSource>().clip = alarmSound;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
