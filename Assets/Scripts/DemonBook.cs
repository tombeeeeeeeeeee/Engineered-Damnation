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
        pageNumber++;
        if (pageNumber >= pages.Count) pageNumber = pages.Count - 1;
        PageTurner();
    }

    public override void Left()
    {
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        PageTurner();
    }

    private void PageTurner()
    {
        Material[] mats = page.materials;
        if (pages[pageNumber])
            mats[1] = pages[pageNumber];
        else
            Debug.LogError("No pages");
        page.materials = mats;
    }

}