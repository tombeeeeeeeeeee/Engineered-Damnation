using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMovingUpScript : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(position.y != transform.position.y) transform.position = position;
        position = transform.position;
    }
}
