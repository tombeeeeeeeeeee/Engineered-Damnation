using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeSequence : SequenceObject
{
    [SerializeField] float strength;
    Vector3 initialPositionRelativeToPlayer;

    private void Start()
    {
        initialPositionRelativeToPlayer = transform.localPosition;
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
    }

    public override void End()
    {
        transform.localPosition = initialPositionRelativeToPlayer;
        base.End();
    }

    protected override void Update()
    {
        base.Update();

        if (inSequence && Gameplay.deltaTime != 0)
        {
            transform.localPosition = initialPositionRelativeToPlayer
                + new Vector3(Random.Range(-strength, strength), Random.Range(-strength, strength), Random.Range(-strength, strength));
        }
    }

    public void SetCameraShakeValues(float strength, float duration)
    {
        this.strength = strength;
        lengthOfOperation = duration;
    }
}
