using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBin : MonoBehaviour
{

    [SerializeField] NewPickup pickupScript;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        // assumes that any object with the CanPickUp tag also has the NewPickup script
        if (go.tag == "CanPickUp")
        {
            if (pickupScript.heldObj == go)
                pickupScript.DropObject();

            if (!go.GetComponent<PickUp>().respawns)
            {
                Destroy(go);
            }
            else
            {
                go.GetComponent<PickUp>().Respawn();
            }
        }
    }
}
