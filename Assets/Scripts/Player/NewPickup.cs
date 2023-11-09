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


    private void NewPickUp(InputAction.CallbackContext context)
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
        if (heldObj)
            MoveObject();
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
            //if the object is attached to something, break it FREEE!!
            if(obj.transform.parent != null)
            {
                SnappingGameObject snappingParent = obj.transform.parent.GetComponent<SnappingGameObject>();
                if (snappingParent != null)
                    snappingParent.ExpectedObject = null;
            }
            pickUpObj.PickedUp();
            
            // Set the holdParent as the parent of the picked object.
            pickUpObj.transform.position = holdParent.position;
            pickUpObj.transform.parent = holdParent;
            heldObj = obj;
        }
    }

    public void DropObject()
    {
        //Grab the object and run its dropping code
        PickUp obj = heldObj.GetComponent<PickUp>();
        obj.Dropped();

        //detattch the object from the player
        heldObj.transform.parent = null;

        //Look for snapping points the object might be able to move to
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(holdParent.position, 0.2f, holdParent.forward, 1f);
        foreach (RaycastHit hit in hits) 
        {
            //Check that the object can snap, and that there already isn't an object in the spot.
            SnappingGameObject snapSpot = hit.collider.gameObject.GetComponent<SnappingGameObject>();
            if (snapSpot != null && snapSpot.SnapType(heldObj) && snapSpot.ExpectedObject == null)
            {
                snapSpot.moving = true;
                heldObj = null;
                return;
            }
        }
        // Remove the holdParent as the parent of the held object.
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

}