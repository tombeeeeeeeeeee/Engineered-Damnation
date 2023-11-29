using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinIcon : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 15 * Time.deltaTime);
    }
}
