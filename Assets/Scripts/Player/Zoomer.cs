using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public Camera cam;
    public float defaultFov = 60;
    public bool tog;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            tog = true;
            cam.fieldOfView = (defaultFov / 4);
        }
        else
        {
            tog = false;
            cam.fieldOfView = (defaultFov);
        }
    }
}
