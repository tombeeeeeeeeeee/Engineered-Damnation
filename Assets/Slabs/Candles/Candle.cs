using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private string LastParent =  "";

    // Start is called before the first frame update
    void Start()
    {
        LastParent = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.parent != null && LastParent != transform.parent.name) || (transform.parent == null && LastParent != ""))
        { 
            LastParent = transform.parent ? transform.parent.name : "";

            RaycastHit hit;
            Physics.Raycast(transform.position, -Vector3.up, out hit, 0.5f);
            if(hit.collider != null && hit.transform.gameObject.GetComponentInChildren<SlabManager>())
            {
                hit.transform.gameObject.GetComponentInChildren<SlabManager>().ChangeCandleToggle(true);
                Destroy(this.gameObject);
            }
        }
    }
}
