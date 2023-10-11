using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonBookUI : MonoBehaviour
{
    public Image page;
    public List<Sprite> pages;
    int pageNumber = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pageNumber++;
            if (pageNumber >= pages.Count) pageNumber = 0;
            page.sprite = pages[pageNumber];
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            pageNumber--;
            if (pageNumber < 0) pageNumber = pages.Count - 1;
            page.sprite = pages[pageNumber];
        }
    }
}
