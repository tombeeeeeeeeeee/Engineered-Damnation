using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] InteractionController controller;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(controller.transform.position);
    }
}
