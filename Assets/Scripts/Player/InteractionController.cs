using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Game Settings")]
    public float pickUpRange = 5;                    // The range within which objects can be picked up.
    public float smoothTime = 0.1f;                  // the time it takes for the object to move
    public Transform holdParent;                     // The transform where the held object will be attached.
    public GameObject heldObj;                       // The currently held object.
    public float rotateSpeed = 20.0f;                 
    private Vector3 moveVelocity = Vector3.zero;     // The force applied to a held object to move it.
    private float startingZ = 1;
    [SerializeReference] FPSController controller;
    [SerializeField] float throwForce;

    public int pixelHeight;
    public int pixelWidth;

    public void Start()
    {
        if(controller.controls == null)
            controller.controls = new Controls();
        controller.controls.Player.Interact.performed += Interaction;
        startingZ = holdParent.transform.localPosition.z;
    }



    private void Interaction(InputAction.CallbackContext context)
    {

        if (heldObj == null)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 10 , 115); //Mask: 01110011

            // Raycast to detect objects with the "CanPickUp" tag within the pickup range.
            if(hit.collider != null)
            {
                //Cylindrical PickUp range
                Vector3 distanceToCollider = hit.transform.position - transform.position;
                Vector2 distanceIgnoringY = new Vector2(distanceToCollider.x, distanceToCollider.z);

                if (distanceIgnoringY.magnitude <= pickUpRange)
                {
                    if (hit.transform.gameObject.tag == "CanPickUp")
                        PickupObject(hit.transform.gameObject);

                    else if (hit.transform.gameObject.tag == "Button")
                        hit.transform.gameObject.GetComponent<SequenceObject>().Begin(true);

                    else if (hit.transform.gameObject.tag == "Focus" && !Gameplay.isFinished)
                        Focus(hit.transform.gameObject);
                }
            }
        }
        else
            // If an object is already held, drop it.
            DropObject(false);
    }


    private void Update() 
    {
        //Move the hold position based off of how far away a collison is.
        RaycastHit hit;
        holdParent.transform.localPosition = new Vector3(0, 0, (Physics.Raycast(transform.position, transform.forward, out hit, startingZ, 179) ? hit.distance : startingZ)); //Mask: 10110011
        //Update position of the held obj
        if (heldObj)
            MoveObject();
    }

    void MoveObject()
    {
        // Apply force to move the held object towards the holdParent.
        heldObj.GetComponent<Rigidbody>().MovePosition(Vector3.SmoothDamp(heldObj.transform.position, holdParent.position, ref moveVelocity, smoothTime));
    }

    public void PickupObject(GameObject obj)
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
            
            // Set the holdParent as the parent of the picked object.
            pickUpObj.transform.position = holdParent.position;
            pickUpObj.idealParent = holdParent;
            heldObj = obj;

            controller.controls.Player.AltInteract.performed += pickUpObj.AlternateInteraction;
            pickUpObj.transform.parent = holdParent;
            pickUpObj.PickedUp();
            pickUpObj.transform.parent = null;
        }
    }

    public void DropObject(bool snapping)
    {
        //Grab the object and run its dropping code
        PickUp obj = heldObj.GetComponent<PickUp>();
        obj.Dropped();

        controller.controls.Player.AltInteract.performed -= obj.AlternateInteraction;
        if(!snapping) obj.GetComponent<Rigidbody>().AddForce((holdParent.position - obj.transform.position) * throwForce * Gameplay.deltaTime, ForceMode.Impulse);

        //detattch the object from the player
        obj.idealParent = null;

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

    public void Focus(GameObject pickObj)
    {

        // trigger camera movemenent to the target object
        // and controls stuff

        pickObj.GetComponent<Focusable>().targetCamera.GetComponent<CameraTransition>().MoveToTarget(controller.controls.Focused);
        pickObj.GetComponent<Focusable>().Init();
    }

}