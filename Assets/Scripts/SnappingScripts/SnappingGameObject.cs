using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGameObject : MonoBehaviour
{
    public bool checkForPlayerGrab;

    [SerializeField] protected GameObject ExpectedObject;
    public NewPickup pickupScript;


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == ExpectedObject)
        {
            if (checkForPlayerGrab && pickupScript.heldObj == other.gameObject) 
                pickupScript.DropObject();
            
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
        }

    }
}
