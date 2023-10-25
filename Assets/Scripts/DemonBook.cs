using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemonBook : MonoBehaviour
{
    public List<Material> pages;
    public FPSController player;

    MeshRenderer page;
    int pageNumber = 0;

    private void Start()
    {
        page = GetComponent<MeshRenderer>();
        player.controls.Focused.Cycle.performed += TurnPage;
    }

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
            Close();
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

    public void Close()
    {
        player.controls.Focused.Disable();
        player.controls.Player.Enable();
    }
}
