using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Focusable : MonoBehaviour
{
    public Camera targetCamera;

    public FPSController player;
    public List<Material> pages;

    MeshRenderer page;
    int pageNumber = 0;


    private void Start()
    {
        page = GetComponent<MeshRenderer>();
        player.controls.Focused.Cycle.performed += Exit;
    }

    private void Exit(InputAction.CallbackContext context)
    {
        // book closed
        if (player.controls.Focused.Cycle.ReadValue<Vector2>().y == 1f)
        {
            targetCamera.GetComponent<CameraTransition>().MoveToPlayer();
            player.locked = false;
        }
    }

    public void NextPage()
    {
        pageNumber++;
        if (pageNumber >= pages.Count) pageNumber = pages.Count - 1;
        page.material = pages[pageNumber];
    }
    public void PreviousPage()
    {
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        page.material = pages[pageNumber];
    }
}
