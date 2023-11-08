using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingGameObject : MonoBehaviour
{

    public bool moving = false;
    [SerializeField] protected GameObject ExpectedObject;
    public NewPickup pickupScript;

    public virtual void Update()
    {
        if (ExpectedObject != null)
        {
            if(moving)
                ExpectedObject.transform.position = Vector3.Lerp(ExpectedObject.transform.position, transform.position, 0.2f);
            else
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
            moving = false;

            if (pickupScript.heldObj == other.gameObject) 
                pickupScript.DropObject();
            
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
            other.transform.SetParent(transform);
        }
    }

    public virtual bool SnapType(GameObject obj)
    {
       return obj == ExpectedObject;
    }
}
