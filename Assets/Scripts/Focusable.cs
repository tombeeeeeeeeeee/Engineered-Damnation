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

    virtual protected void Start()
    {
        player.controls.Focused.Cycle.performed += TurnPage;
    }

    // handling input
    // the only one that does anything in thsi base class is exit
    // other classes can inherit this and override the virtual methods
    private void TurnPage(InputAction.CallbackContext context)
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

        // book closed
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().y == 1f)
        {
            Exit();
        }
    }

    virtual public void NextPage()
    {
        Debug.Log("next");
    }

    virtual public void PreviousPage()
    {
        Debug.Log("prev");
    }

    private void Exit()
    {
        Debug.Log(targetCamera.gameObject.name);
        targetCamera.GetComponent<CameraTransition>().MoveToPlayer();
        player.locked = false;
    }
}
