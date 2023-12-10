using UnityEngine;
using UnityEngine.InputSystem;

public class UnfocusSequence : SequenceObject
{
    [SerializeField] Focusable[] inGameFocusObjects;

    public override void Begin(bool decision)
    {
        foreach(Focusable focusable in inGameFocusObjects)
        {
            if(focusable.targetCamera.enabled)
            {
                focusable.Exit(new InputAction.CallbackContext());
                lengthOfOperation = focusable.targetCamera.gameObject.GetComponent<CameraTransition>().duration;
                break;
            }
        }
        base.Begin(decision);
    }
}
