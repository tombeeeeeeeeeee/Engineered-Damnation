using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemonBookUI : MonoBehaviour
{
    public Image page;
    public List<Sprite> pages;
    int pageNumber = 0;
    public FPSController player;

    private void Start()
    {
        player.controls.Focused.Cycle.performed += TurnPage;
    }

    private void TurnPage(InputAction.CallbackContext context)
    {
        // the book controls are a Vector2
        // x axis (A and D) are used for page turning
        // up on y axis is mapped to esc and is used to close the book
        // (might change to seperate input later)


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
        page.sprite = pages[pageNumber];
    }
    public void PreviousPage()
    {
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        page.sprite = pages[pageNumber];
    }

    public void Close()
    {
        player.controls.Player.Enable();
        gameObject.SetActive(false);
    }
}
