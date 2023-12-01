using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinSequence : SequenceObject
{
    [HideInInspector]
    public GameObject spinningObject;
    public float spinSpeedMultiplier;


    [SerializeField] ParticleSystem particleEffects;
    [SerializeField] AudioClip SummoningSongWin;
    [SerializeField] AudioClip SummoningSongLose;

    // Update is called once per frame
    protected override void Update()
    {
        if(spinningObject != null)
            spinningObject.transform.Rotate(0, timeInOperation * spinSpeedMultiplier * Gameplay.deltaTime, 0);
        if (inSequence)
            spinningObject.transform.position = transform.position;

        //particleEffects.startLifetime = timeInOperation * 3 /  lengthOfOperation;

        base.Update();
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            GetComponent<AudioSource>().clip = (decision ? SummoningSongWin : SummoningSongLose);
            GetComponent<AudioSource>().Play();
            particleEffects.gameObject.SetActive(true);
        }
    }

    public override void End()
    {
        particleEffects.gameObject.SetActive(false);
        spinningObject.GetComponent<AudioSource>().Stop();
        if (nextInSequence.gameObject.GetComponent<FlickerDecisionLight>() != null)
            nextInSequence.gameObject.GetComponent<FlickerDecisionLight>().objectToDestroy = spinningObject; 
        base.End();
    }

}
