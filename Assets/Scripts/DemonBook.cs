using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DemonBook : Focusable
{
    public List<Material> pages;
    [SerializeField] AudioClip PagemoveSound;
    [SerializeField] Image LeftArrow;
    [SerializeField] Image RightArrow;
    [SerializeField] AudioClip[] pageTurnSounds;

    MeshRenderer page;
    int pageNumber = 0;

    override public void Init()
    {
        page = GetComponent<MeshRenderer>();
        base.Init(); // base class adds the turnpage function with init so this will too
    }


    public override void Right()
    {
        if(pageNumber < pages.Count - 1)
        {
            pageNumber++;
            PageTurner(); 
        }
    }

    public override void Left()
    {
        if (pageNumber > 0)
        {
            pageNumber--;
            PageTurner();
        }
    }

    private void PageTurner()
    {
        Material[] mats = page.materials;
        if (pages[pageNumber])
            mats[1] = pages[pageNumber];
        GetComponent<AudioSource>().PlayOneShot(pageTurnSounds[Random.Range(0,pageTurnSounds.Length)]);
        LeftArrow.enabled = !(pageNumber == 0);
        RightArrow.enabled = !(pageNumber == pages.Count - 1);

        page.materials = mats;
    }

}