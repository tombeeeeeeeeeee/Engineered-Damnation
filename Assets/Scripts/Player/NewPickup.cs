using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class NewPickup : MonoBehaviour
{
    [Header("Game Settings")]
    public float pickUpRange = 5;                    // The range within which objects can be picked up.
    public float smoothTime = 0.1f;                  // the time it takes for the object to move
    public Transform holdParent;                     // The transform where the held object will be attached.
    public GameObject heldObj;                       // The currently held object.
    public float rotateSpeed = 20.0f;                 
    private Vector2 rotation = Vector2.zero;
    private Vector3 moveVelocity = Vector3.zero;     // The force applied to a held object to move it.



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

                else if (hit.transform.gameObject.tag == "Button")
                    hit.transform.gameObject.GetComponent<WorldSpaceButton>().Press();


                else if (hit.transform.gameObject.tag == "Focus")
                    Focus(hit.transform.gameObject);
            }
        }
        else
        {
            // If an object is already held, drop it.
            DropObject();
        }
    }

    private void Update() // not sure if this needs to be Update or should be FixedUpdate (or if it matters at all)
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


    void PickupObject(GameObject obj)
    {
        PickUp pickUpObj = obj.GetComponent<PickUp>();
        if (pickUpObj)
        {
            pickUpObj.PickedUp();
            
            // Set the holdParent as the parent of the picked object.
            pickUpObj.transform.position = holdParent.position;
            pickUpObj.transform.parent = holdParent;
            heldObj = obj;
        }
    }

    public void DropObject()
    {
        PickUp obj = heldObj.GetComponent<PickUp>();
        obj.Dropped();

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(holdParent.position, 0.1f, holdParent.forward, 0.5f);
        foreach (RaycastHit hit in hits) 
        {
            SnappingGameObject snapSpot = hit.collider.gameObject.GetComponent<SnappingGameObject>();
            if (snapSpot != null && snapSpot.SnapType(heldObj))
            {
                snapSpot.moving = true;
                heldObj = null;
                return;
            }
        }

        // Remove the holdParent as the parent of the held object.
        heldObj.transform.parent = null;
        heldObj = null;
    }

    public void OpenBook(GameObject pickObj)
    {
        // the GameObject with tag "DemonBook" is passed in
        // but currently isn't used for anything
        transform.parent.GetComponent<FPSController>().Focus();
    }

    public void Focus(GameObject pickObj)
    {
        //transform.parent.GetComponent<FPSController>().FocusCamera(pickObj.GetComponent<Focusable>().targetCamera);
        transform.parent.GetComponent<FPSController>().locked = true;
        // trigger camera movemenent to the target object
        // and controls stuff
        pickObj.GetComponent<Focusable>().targetCamera.GetComponent<CameraTransition>().MoveToTarget();
        pickObj.GetComponent<Focusable>().Init();
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