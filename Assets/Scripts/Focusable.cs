using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Focusable : MonoBehaviour
{
    public Camera targetCamera;
    public FPSController player;

    // this is called when the object is clicked on by player
    // inhertiing class demonbook calls back to base for this too
    public virtual void Init()
    {
        player.controls.Focused.Cycle.performed += Cycle;
        player.controls.Focused.Action1.performed += Action1;
        player.controls.Focused.Action2.performed += Action2;
    }

    // handling input
    // the only one that does anything in thsi base class is exit
    // other classes can inherit this and override the virtual methods
    private void Cycle(InputAction.CallbackContext context)
    {
        // page turned next
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().x == 1f)
        {
            NextPage();
        }
        // page turned previous
        else if (player.controls.Focused.Cycle.ReadValue<Vector2>().x == -1f)
        {
            PreviousPage();
        }
        // closed
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().y == 1f)
        {
            Exit();
        }
    }

    virtual public void NextPage()
    {
        // override if you want this to do something
    }

    virtual public void PreviousPage()
    {
        // override if you want this to do something
    }

    virtual public void Action1(InputAction.CallbackContext context)
    {
        // override if you want this to do something
    }

    virtual public void Action2(InputAction.CallbackContext context)
    {
        // override if you want this to do something
    }

    public void Exit()
    {
        player.controls.Focused.Cycle.performed -= Cycle;
        targetCamera.GetComponent<CameraTransition>().MoveToPlayer();
        player.locked = false;
    }
}
