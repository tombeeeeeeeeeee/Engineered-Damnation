using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlabButton : WorldSpaceButton
{
    [SerializeReference] ToolSpawner toolSpawner;
    [SerializeField] float PressCooldown;
    float LastPressedTime = 0;
    public override void Press()
    {
        if(LastPressedTime + PressCooldown < Time.time)
        {
            LastPressedTime = Time.time;
            toolSpawner.SpawnTool();
        }
    }
}
