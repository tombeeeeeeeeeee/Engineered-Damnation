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
    public Sprite interact;

    public float pickUpRange;

    private void Start()
    {
        pickUpRange = player.GetComponent<InteractionController>().pickUpRange;
    }

    private void Update()
    {
        RaycastHit hit;
        crosshair.enabled = playerCamera.GetComponent<Camera>().enabled;
        

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickUpRange, 115))
        {
            switch (hit.transform.gameObject.tag)
            {
                default:
                    crosshair.sprite = normal;
                    break;

                case "CanPickUp":
                    crosshair.sprite = grab;
                    break;

                case "Focus":
                    crosshair.sprite = interact;
                    break;
            }
        }
        else crosshair.sprite = normal;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);

        // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
    }
}
