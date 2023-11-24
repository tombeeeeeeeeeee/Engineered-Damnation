using UnityEngine;
using UnityEngine.InputSystem;

public class Focusable : MonoBehaviour
{
    public Camera targetCamera;
    public FPSController player;
    public GameObject ui;

    // this is called when the object is clicked on by player
    // inhertiing class demonbook calls back to base for this too
    public virtual void Init()
    {
        player.controls.Focused.Cycle.performed += Cycle;
        player.controls.Focused.Action2.performed += Action2;
        player.controls.Focused.Exit.performed += Exit;

        Invoke("ShowUI", targetCamera.gameObject.GetComponent<CameraTransition>().duration);
        Invoke("OnFocus", targetCamera.gameObject.GetComponent<CameraTransition>().duration);
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

    virtual public void OnFocus()
    {
        // override if you want this to do something
    }

    virtual public void OnUnfocus()
    {
        // override if you want this to do something
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
        OnUnfocus();
        player.controls.Focused.Cycle.performed -= Cycle;
        targetCamera.GetComponent<CameraTransition>().MoveToPlayer();
        ui.SetActive(false);
    }

    void ShowUI()
    {
        ui.SetActive(true);
    }
}
