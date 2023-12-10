using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public GameObject playerCamera;
    public Image crosshair;

    public SpriteAlternate rotationUI;

    public float pickUpRange;

    private void Start()
    {
        pickUpRange = playerCamera.GetComponent<InteractionController>().pickUpRange;
    }

    private void Update()
    {
        RaycastHit hit;
        crosshair.enabled = playerCamera.GetComponent<Camera>().enabled;
        
        bool crosshairON = false;

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10, 115))
        {
            //Cylindrical PickUp range
            Vector3 distanceToCollider = hit.transform.position - transform.position;
            Vector2 distanceIgnoringY = new Vector2(distanceToCollider.x, distanceToCollider.z);

            if (distanceIgnoringY.magnitude <= pickUpRange && playerCamera.GetComponent<InteractionController>().heldObj == null)
            {
            	switch (hit.transform.gameObject.tag)
            	{
                    case "Button":
                	case "CanPickUp":
                	case "Focus":
                        crosshairON = true;
                        break;
				}
            }
        }
        crosshair.enabled = crosshairON;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
        // Raycast to detect objects with the "CanPickUp" tag within the pickup range.


        if(playerCamera.GetComponent<InteractionController>().heldObj != null 
            && playerCamera.GetComponent<InteractionController>().heldObj.GetComponent<Potion>() != null)
        {
            rotationUI.gameObject.SetActive(true);
        } else rotationUI.gameObject.SetActive(false);
    }
}
