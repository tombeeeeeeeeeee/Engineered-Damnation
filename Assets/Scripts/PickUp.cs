using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour
{
    [SerializeField] int ignoreRaycast;


    //Rigidbody properties:
    private Rigidbody rb;
    private bool usesGravity;
    private float drag;
    private Vector3 angularVelocity;
    private bool freezeRotation;
    private int defaultLayer;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        usesGravity = rb.useGravity;
        drag = rb.drag;
        angularVelocity = rb.angularVelocity;
        freezeRotation = rb.freezeRotation;
        defaultLayer = gameObject.layer;
    }


    public virtual void PickedUp()
    {
        // Disable gravity, increase drag, and freeze rotation to simulate holding.
        rb.useGravity = false;
        rb.drag = 10;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = true;

        //ignore raycast so that the player can raycast through it.
        gameObject.layer = ignoreRaycast;
    }

    public virtual void Dropped()
    {
        //resets rigidbody to before being picked up.
        rb.useGravity = usesGravity;
        rb.drag = drag;
        rb.angularVelocity = angularVelocity;
        rb.freezeRotation = freezeRotation;

        gameObject.layer = defaultLayer;
    }
}
