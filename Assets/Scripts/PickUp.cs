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
    public bool hasBeenAlt = false;
    [HideInInspector] public Transform idealParent;

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
        defaultLayer = gameObject.layer = 6;
    }

    protected virtual void Update()
    {
        if (!hasBeenAlt && idealParent != null)
            transform.rotation = Quaternion.LookRotation(-idealParent.forward, idealParent.up);
    }

    public virtual void PickedUp()
    {
        // Disable gravity, increase drag, and freeze rotation to simulate holding.
        rb.useGravity = false;
        rb.drag = 10;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.isKinematic = false;

        //ignore raycast so that the player can raycast through it.
        Gameplay.ChildrenLayerSet(gameObject, 2);
    }

    public virtual void Dropped()
    {
        hasBeenAlt = false;

        //resets rigidbody to before being picked up.
        rb.useGravity = usesGravity;
        rb.drag = drag;
        rb.angularVelocity = angularVelocity;
        rb.freezeRotation = freezeRotation;

        Gameplay.ChildrenLayerSet(gameObject, defaultLayer);
    }

    public virtual void AlternateInteraction(InputAction.CallbackContext context)
    {
        hasBeenAlt = !hasBeenAlt;
        if(!hasBeenAlt)
            transform.rotation = Quaternion.LookRotation(-idealParent.forward, idealParent.up);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        int index = Random.Range(0, collisionSounds.Length);
        if (collisionSounds.Length > 0 && Gameplay.active)
            aS.PlayOneShot(collisionSounds[index]);
    }

}
