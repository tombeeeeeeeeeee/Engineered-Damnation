using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBin : MonoBehaviour
{
    public List<GameObject> destructibleObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Destructable>())
        {
            Destroy(other.gameObject);
        }
    }
}
