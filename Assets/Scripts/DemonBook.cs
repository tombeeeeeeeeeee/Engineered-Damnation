using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemonBook : Focusable
{
    public List<Material> pages;

    MeshRenderer page;
    int pageNumber = 0;

    private void Start()
    {
        page = GetComponent<MeshRenderer>();
    }

    public override void NextPage()
    {
        Debug.Log("next");
        pageNumber++;
        if (pageNumber >= pages.Count) pageNumber = pages.Count - 1;
        page.material = pages[pageNumber];
    }
    public override void PreviousPage()
    {
        Debug.Log("previous");
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        page.material = pages[pageNumber];
    }
}