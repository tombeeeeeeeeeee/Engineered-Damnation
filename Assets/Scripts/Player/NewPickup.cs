using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class NewPickup : MonoBehaviour
{
    [Header("Game Settings")]
    public float pickUpRange = 5;       // The range within which objects can be picked up.
    public float smoothTime = 0.1f;     // the time it takes for the object to move
    public Transform holdParent;        // The transform where the held object will be attached.
    public GameObject heldObj;          // The currently held object.
    public GameObject bookUI;
    public float rotateSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;
    private Vector3 moveVelocity = Vector3.zero;    // The force applied to a held object to move it.



    [SerializeReference] FPSController controller;

    private void Start()
    {
        if(controller.controls == null)
            controller.controls = new Controls();

        controller.controls.Player.Interact.performed += NewPickUp;
    }


    void NewPickUp(InputAction.CallbackContext context)
    {

        if (heldObj == null)
        {
            RaycastHit hit;
            
            Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange);
            // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
            if(hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "CanPickUp")
                    PickupObject(hit.transform.gameObject);

                else if (hit.transform.gameObject.tag == "ToolSpawner")
                {
                    GameObject tool = Instantiate(hit.transform.gameObject.GetComponent<ToolSpawner>().getToolFromCollection(), holdParent);
                    PickupObject(tool);
                }

                else if (hit.transform.gameObject.tag == "Button")
                    hit.transform.gameObject.GetComponent<WorldSpaceButton>().Press();

                else if (hit.transform.gameObject.tag == "DemonBook")
                    OpenBook(hit.transform.gameObject);
            }
        }
        else
        {
            // If an object is already held, drop it.
            DropObject();
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if (heldObj)
            MoveObject();

        controller.rotationLocked = controller.controls.Player.Rotate.IsPressed();

        if(controller.rotationLocked)
            RotateObject();
    }

    void MoveObject()
    {
        // Apply force to move the held object towards the holdParent.
        heldObj.transform.position = Vector3.SmoothDamp(heldObj.transform.position, holdParent.position, ref moveVelocity, smoothTime);
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
            objRig.transform.position = holdParent.position;
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

    public void OpenBook(GameObject pickObj)
    {
        // the GameObject with tag "DemonBook" is passed in
        // but currently isn't used for anything
        if (!bookUI.activeInHierarchy)
        {
            bookUI.SetActive(true);
        }
    }

    public void RotateObject()
    {
        if(heldObj != null)
        {
            rotation = controller.controls.Player.Camera.ReadValue<Vector2>() * rotateSpeed * Time.deltaTime;

            heldObj.transform.Rotate(transform.up, -rotation.x, Space.World);
            heldObj.transform.Rotate(transform.right, rotation.y, Space.World);
        }
    }
}