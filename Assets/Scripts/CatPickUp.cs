using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatPickUp : PickUp
{
    [SerializeField] AudioClip[] meows;

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        base.AlternateInteraction(context);
        aS.PlayOneShot(meows[Random.Range(0, meows.Length)]);
    }
}
