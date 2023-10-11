using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewPickup : MonoBehaviour
{
    [Header("Game Settings")]
    public float pickUpRange = 5;       // The range within which objects can be picked up.
    public float moveForce = 250;       // The force applied to a held object to move it.
    public Transform holdParent;        // The transform where the held object will be attached.
    public GameObject heldObj;          // The currently held object.
    public GameObject bookUI;

    public float rotateSpeed = 2.0f;
    float rotationX = 0;
    float rotationY = 0;

    // Update is called once per frame
    void Update()
    {
        // Debug Raycast to visualize the pickup range.
        Vector3 forward = transform.TransformDirection(Vector3.forward) * pickUpRange;
        Debug.DrawRay(transform.position, forward, Color.red);

        // Check for the "E" key press to pick up or drop an object.
        if (Input.GetMouseButtonDown(0) /*Input.GetKeyDown(KeyCode.E)*/)
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange);

                // If an object is hit, pick it up.
                if (hit.transform.gameObject.tag == "CanPickUp")
                    PickupObject(hit.transform.gameObject);
                else if (hit.transform.gameObject.tag == "DemonBook")
                    InteractObject(hit.transform.gameObject);
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

        // Object rotation with the mouse while R is down
        if (Input.GetKey(KeyCode.R))
        {
            rotationX = Input.GetAxis("Mouse X") * rotateSpeed;
            rotationY = Input.GetAxis("Mouse Y") * rotateSpeed;

            heldObj.transform.Rotate(transform.up, -rotationX, Space.World);
            heldObj.transform.Rotate(transform.right, rotationY, Space.World);
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
            objRig.angularVelocity = Vector3.zero;
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

    public void InteractObject(GameObject pickObj)
    {
        if (!bookUI.activeInHierarchy)
        {
            bookUI.SetActive(true);
        }
    }
}