using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public GameObject playerCameraObject;
    public Image crosshair;

    public Sprite normal;
    public Sprite grab;
    public Sprite grabbed;
    public Sprite interact;

    public float pickUpRange;

    Camera playerCamera;
    InteractionController playerInteractionController;

    private void Start()
    {
        pickUpRange = playerCameraObject.GetComponent<InteractionController>().pickUpRange;
        playerCamera = playerCameraObject.GetComponent<Camera>();
        playerInteractionController = playerCamera.GetComponent<InteractionController>();
    }

    private void Update()
    {
        RaycastHit hit;
        crosshair.enabled = playerCamera.enabled;

        if (playerInteractionController.heldObj != null)
        {
            crosshair.sprite = grabbed;
        }
        else
        {
            if (Physics.Raycast(playerCameraObject.transform.position, playerCameraObject.transform.forward, out hit, pickUpRange, 115))
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
            Debug.DrawRay(playerCameraObject.transform.position, playerCameraObject.transform.forward, Color.red);

            // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
        }
    }
}
