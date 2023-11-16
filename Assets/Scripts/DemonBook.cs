using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemonBook : Focusable
{
    public List<Material> pages;

    MeshRenderer page;
    int pageNumber = 0;

    override public void Init()
    {
        page = GetComponent<MeshRenderer>();
        base.Init(); // base class adds the turnpage function with init so this will too
    }

    public override void Right()
    {
        Debug.Log("next");
        pageNumber++;
        if (pageNumber >= pages.Count) pageNumber = pages.Count - 1;
        page.material = pages[pageNumber];
    }

    public override void Left()
    {
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        page.material = pages[pageNumber];
    }
}