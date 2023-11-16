using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossWinOrLoseSequence : SequenceObject
{
    [SerializeField] AudioClip winningVLine;
    [SerializeField] AudioClip losingVLine;
    private AudioSource intercom;
    [SerializeField] SequenceObject winStateSequence;
    [SerializeField] SequenceObject losingStateSequence;

    private void Start()
    {
        intercom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inSequence)
        {
            timeInOperation += Time.deltaTime;
            if(timeInOperation > lengthOfOperation)
            {
                inSequence = false;
                End();
            }
        }
    }

    public override void Begin(bool decision)
    {
        this.decision = decision;
        nextInSequence = decision ? winStateSequence : losingStateSequence;

        if(!inSequence)
        {
            inSequence = true;
            timeInOperation = 0;

            AudioClip clip = decision ? winningVLine : losingVLine;
            lengthOfOperation = clip.length;
            intercom.PlayOneShot(clip);
        }
    }

    public override void End()
    {
        nextInSequence.Begin(decision);
    }
}
