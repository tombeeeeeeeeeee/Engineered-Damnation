using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBin : MonoBehaviour
{

    [SerializeField] InteractionController pickupScript;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        // assumes that any object with the CanPickUp tag also has the InteractionController script
        if (go.GetComponent<PickUp>())
        {
            if (pickupScript.heldObj == go)
                pickupScript.DropObject();

            if (go.GetComponent<Potion>())
                go.GetComponent<Potion>().Respawn();
            else
                Destroy(go);
        }
    }
}
