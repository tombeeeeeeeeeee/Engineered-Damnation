using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGameObject : MonoBehaviour
{

    public bool snapped = false;
    [SerializeField] protected GameObject ExpectedObject;
    public NewPickup pickupScript;

    public virtual void Update()
    {
        if (ExpectedObject != null)
        {
            snapped = ExpectedObject.transform.parent == this.transform;
            if (snapped)
            {
                ExpectedObject.transform.rotation = transform.rotation;
                ExpectedObject.transform.position = transform.position;
            }
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == ExpectedObject)
        {
            if (pickupScript.heldObj == other.gameObject) 
                pickupScript.DropObject();
            
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
            other.transform.SetParent(transform);
        }
    }
}
