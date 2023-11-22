using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSummonSequence : SequenceObject
{
    [SerializeField] DemonSummoningSpot dSP;
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
            Destroy(dSP.ExpectedObject);
        }
    }

    public override void End()
    {
        dSP.ExpectedObject = null;
        dSP.summoning = false;
        base.End();
    }
}
