using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PickUp : MonoBehaviour
{
    //Sounds
    [SerializeField] protected AudioClip[] collisionSounds;
    protected AudioSource aS;

    //Rigidbody properties:
    private Rigidbody rb;
    private bool usesGravity;
    private float drag;
    private Vector3 angularVelocity;
    private bool freezeRotation;
    private int defaultLayer;
    protected bool hasBeenAlt = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Audio
        aS = GetComponent<AudioSource>();

        //Rigid Body Properties
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
        gameObject.layer = 2;
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

    public virtual void AlternateInteraction(InputAction.CallbackContext context)
    {
        if(!hasBeenAlt)
            transform.rotation = Quaternion.LookRotation(transform.parent.up, -transform.parent.forward);
        else
            transform.rotation = Quaternion.LookRotation(-transform.parent.forward, transform.parent.up);
        hasBeenAlt = !hasBeenAlt;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        int index = Random.Range(0, collisionSounds.Length);
        aS.PlayOneShot(collisionSounds[index]);
    }

}
