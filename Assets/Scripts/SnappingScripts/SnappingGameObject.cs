using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGameObject : MonoBehaviour
{

    public bool moving = false;
    public GameObject ExpectedObject;
    public NewPickup pickupScript;

    public virtual void Update()
    {
        //If we have an object we are expecting to collide with
        if (ExpectedObject != null)
        {
            //and the object is moving into this spot
            if(moving)
            {
                //move it to correct spot ignoring gravity
                ExpectedObject.GetComponent<Rigidbody>().useGravity = false;
                ExpectedObject.transform.position = Vector3.Lerp(ExpectedObject.transform.position, transform.position, 0.01f);
            }
                
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == ExpectedObject)
        {
            //Stop moving the object to the correct spot
            moving = false;
            other.GetComponent<Rigidbody>().useGravity = true;

            //if the object came from a player, take it away from them.
            if (pickupScript.heldObj == other.gameObject) 
                pickupScript.DropObject();

            //put the object in the right spot.
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
            other.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Returns the expecting type of object that snaps
    /// </summary>
    /// <param name="obj"> Gameobject to check</param>
    /// <returns>true if input matches expected type</returns>
    public virtual bool SnapType(GameObject obj)
    {
       return obj == ExpectedObject;
    }
}
