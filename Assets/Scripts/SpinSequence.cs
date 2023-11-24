using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinSequence : SequenceObject
{
    [HideInInspector]
    public GameObject spinningObject;
    public float spinSpeedMultiplier;

    [SerializeField] AudioClip[] Whispers;

    // Update is called once per frame
    protected override void Update()
    {
        if(spinningObject != null)
            spinningObject.transform.Rotate(0, timeInOperation * spinSpeedMultiplier * Gameplay.deltaTime, 0);        
        base.Update();
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            spinningObject.GetComponent<AudioSource>().clip = Whispers[Random.Range(0, Whispers.Length)];
            spinningObject.GetComponent<AudioSource>().Play();
        }
    }

    public override void End()
    {
        spinningObject.GetComponent<AudioSource>().Stop();
        if (nextInSequence.gameObject.GetComponent<FlickerDecisionLight>() != null)
            nextInSequence.gameObject.GetComponent<FlickerDecisionLight>().objectToDestroy = spinningObject; 
        base.End();
    }

}
