using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public Image crosshair;

    public Sprite normal;
    public Sprite grab;
    public Sprite book;
    public Sprite dial;

    public float pickUpRange;

    private void Start()
    {
        pickUpRange = player.GetComponent<InteractionController>().pickUpRange;
    }

    private void Update()
    {
        RaycastHit hit;
        crosshair.enabled = Gameplay.gameplayActive;
        

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickUpRange, 115))
        {
            if (hit.transform.gameObject.tag == "CanPickUp")
            {
                crosshair.sprite = grab;
            }
            else crosshair.sprite = normal;
        }
        else crosshair.sprite = normal;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);

        // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
    }
}
