using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossWinOrLoseSequence : SequenceObject
{
    [Header("Win")]
    [SerializeField] AudioClip winningVLine;
    [SerializeField] SequenceObject winStateSequence;

    [Header("Lose")]
    [SerializeField] SequenceObject losingStateSequence;
    [SerializeField] AudioClip losingVLine;

    private AudioSource intercom;

    private void Start()
    {
        intercom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (inSequence)
        {
            base.Update();
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        nextInSequence = decision ? winStateSequence : losingStateSequence;

        if(!inSequence)
        {
            AudioClip clip = decision ? winningVLine : losingVLine;
            lengthOfOperation = clip.length;
            intercom.PlayOneShot(clip);
        }
    }
}
