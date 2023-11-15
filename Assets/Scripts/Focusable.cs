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
        player.controls.Focused.Action2.performed += Action2;
        player.controls.Focused.Exit.performed += Exit;
    }

    // handling input
    // the only one that does anything in thsi base class is exit
    // other classes can inherit this and override the virtual methods
    private void Cycle(InputAction.CallbackContext context)
    {
        // page turned next
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().x == 1f)
        {
            Right();
        }
        // page turned previous
        else if (player.controls.Focused.Cycle.ReadValue<Vector2>().x == -1f)
        {
            Left();
        }
        // Updown
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().y != 0)
        {
            UpDown(player.controls.Focused.Cycle.ReadValue<Vector2>().y);
        }
    }

    virtual public void Right()
    {
        // override if you want this to do something
    }

    virtual public void Left()
    {
        // override if you want this to do something
    }

    virtual public void UpDown(float userInput)
    {
        // override if you want this to do something
    }

    virtual public void Action2(InputAction.CallbackContext context)
    {
        // override if you want this to do something
    }

    public virtual void Exit(InputAction.CallbackContext context)
    {
        player.controls.Focused.Cycle.performed -= Cycle;
        targetCamera.GetComponent<CameraTransition>().MoveToPlayer();
    }
}
