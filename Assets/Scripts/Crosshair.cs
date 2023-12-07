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

    public SpriteAlternate rotationUI;

    public float pickUpRange;

    private void Start()
    {
        pickUpRange = player.GetComponent<InteractionController>().pickUpRange;
    }

    private void Update()
    {
        RaycastHit hit;
        crosshair.enabled = playerCamera.GetComponent<Camera>().enabled;
        

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10, 115))
        {
            //Cylindrical PickUp range
            Vector3 distanceToCollider = hit.transform.position - transform.position;
            Vector2 distanceIgnoringY = new Vector2(distanceToCollider.x, distanceToCollider.z);

            if (distanceIgnoringY.magnitude <= pickUpRange)
            {
                if (hit.transform.gameObject.tag == "CanPickUp")
                {
                    crosshair.sprite = grab;
                }
                else crosshair.sprite = normal;
            }
            else crosshair.sprite = normal;
        }
        else crosshair.sprite = normal;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
        // Raycast to detect objects with the "CanPickUp" tag within the pickup range.


        if(playerCamera.GetComponent<InteractionController>().heldObj != null 
            && playerCamera.GetComponent<InteractionController>().heldObj.GetComponent<Potion>() != null)
        {
            rotationUI.gameObject.SetActive(true);
        } else rotationUI.gameObject.SetActive(false);
    }
}
