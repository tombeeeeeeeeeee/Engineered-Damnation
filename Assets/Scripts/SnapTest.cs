using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTest : MonoBehaviour
{
    public bool checkForPlayerGrab;

    public string objectName;
    public NewPickup pickupScript;
    public GameObject snapPos;

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == objectName)
        {
            if (checkForPlayerGrab && pickupScript.heldObj == other.gameObject)
            {
                pickupScript.DropObject();
                other.gameObject.transform.rotation = snapPos.transform.rotation;
                other.gameObject.transform.position = snapPos.transform.position;
            }
            else
            {
                other.gameObject.transform.rotation = snapPos.transform.rotation;
                other.gameObject.transform.position = snapPos.transform.position;
            }
        }

    }
}
