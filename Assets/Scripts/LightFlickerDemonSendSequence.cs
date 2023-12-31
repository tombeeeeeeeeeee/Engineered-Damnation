﻿using UnityEngine;
public class LightFlickerDemonSendSequence:LightFlickerOffSequence
{
    public GameObject objectToDestroy;
    [SerializeField] DemonSummoningSpot dSS;

    private void Start()
    {
        lightToFlicker = GetComponent<Light>();
    }

    protected override void Update()
    {
        if (numberOfFlickers == 1 && objectToDestroy && !Gameplay.isFinished)
        {
            if (objectToDestroy.GetComponent<Demon>())
                objectToDestroy.GetComponent<Demon>().Vanish();
        }
        base.Update();
    }

    public override void End()
    {
        if (inSequence)
        {
            dSS.ExpectedObject = null;
            dSS.summoning = false;
        }
        base.End();
    }
}