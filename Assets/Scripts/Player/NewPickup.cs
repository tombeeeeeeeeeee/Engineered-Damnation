using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPickup : MonoBehaviour
{
    [Header("Game Settings")]
    public float pickUpRange = 5;       // The range within which objects can be picked up.
    public float moveForce = 250;       // The force applied to a held object to move it.
    public Transform holdParent;        // The transform where the held object will be attached.
    public GameObject heldObj;          // The currently held object.

    // Update is called once per frame
    void Update()
    {
        // Debug Raycast to visualize the pickup range.
        Vector3 forward = transform.TransformDirection(Vector3.forward) * pickUpRange;
        Debug.DrawRay(transform.position, forward, Color.red);

        // Check for the "E" key press to pick up or drop an object.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                
                Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange);
                // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
                if (hit.transform.gameObject.tag == "CanPickUp")
                    PickupObject(hit.transform.gameObject);

                else if (hit.transform.gameObject.tag == "ToolSpawner")
                {
                    GameObject tool = Instantiate(hit.transform.gameObject.GetComponent<ToolSpawner>().getToolFromCollection(), holdParent);
                    PickupObject(tool);
                }

                else if (hit.transform.gameObject.tag == "Button")
                    hit.transform.gameObject.GetComponent<WorldSpaceButton>().Press();

            }
            else
            {
                // If an object is already held, drop it.
                DropObject();
            }
        }

        if (heldObj != null)
        {
            // If an object is held, move it.
            MoveObject();
        }
    }

    void MoveObject()
    {
        // Calculate the distance between the held object and the holdParent.
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            // Apply force to move the held object towards the holdParent.
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            // Disable gravity, increase drag, and freeze rotation to simulate holding.
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.freezeRotation = true;

            // Set the holdParent as the parent of the picked object.
            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }

    public void DropObject()
    {
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        // Enable gravity, reset drag, and unfreeze rotation to drop the object.
        heldRig.useGravity = true;
        heldRig.drag = 1;
        heldRig.freezeRotation = false;

        // Remove the holdParent as the parent of the held object.
        heldObj.transform.parent = null;
        heldObj = null;
    }
}