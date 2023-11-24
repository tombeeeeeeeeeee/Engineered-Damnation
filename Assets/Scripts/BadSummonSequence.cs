using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSummonSequence : SequenceObject
{
    [SerializeField] DemonSummoningSpot dSS;
    [SerializeField] AudioClip badSummonSound;
    private AudioSource aS;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            lengthOfOperation = badSummonSound.length;
            aS.PlayOneShot(badSummonSound);
        }
    }

    public override void End()
    {
        base.End();
        dSS.ExpectedObject = null;
        dSS.summoning = false;
    }
}
