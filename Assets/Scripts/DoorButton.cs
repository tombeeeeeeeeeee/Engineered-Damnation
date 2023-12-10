using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : SequenceObject
{
    [SerializeField] AudioClip lockedDoorSound;
    private float timeSinceDoorKnock = 0;

    protected override void Update()
    {
        timeSinceDoorKnock += Gameplay.deltaTime;
        base.Update();
        if (inSequence && timeSinceDoorKnock > 0.25f)
        {
            timeSinceDoorKnock = 0;
            gameObject.GetComponent<AudioSource>().PlayOneShot(lockedDoorSound);
        }
    }

    public override void Begin(bool decision)
    {
        if (!inSequence)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(lockedDoorSound);
            lengthOfOperation = lockedDoorSound.length * 2;
            gameObject.tag = "Untagged";
        }

        base.Begin(decision);
    }

    public override void End()
    {
        gameObject.tag = "Button";
        base.End();
    }
}
