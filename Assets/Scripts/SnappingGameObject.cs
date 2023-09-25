using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGameObject : MonoBehaviour
{
    public bool checkForPlayerGrab;

    [SerializeField] GameObject ExpectedObject;
    public NewPickup pickupScript;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ExpectedObject)
        {
            if (checkForPlayerGrab && pickupScript.heldObj == other.gameObject) 
                pickupScript.DropObject();
            
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
        }

    }
}
