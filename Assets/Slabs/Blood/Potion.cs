using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Potion : PickUp
{

    public uint LiquidKey = 0;
    public Color LiquidColour;
    [SerializeReference] protected Transform pourPosition;
    [SerializeField] protected float pourRadius;
    [SerializeField] protected float pourAngle;

    public GameObject cork;
    public GameObject particle1;
    public GameObject particle2;

    public AudioClip[] pourSounds;
    public AudioClip pourEnd;

    Vector3 spawnPosition;
    Quaternion spawnRotation;

    protected override void Start()
    {
        base.Start();
        cork.SetActive(true);
        particle1.SetActive(false);
        particle2.SetActive(false);
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

    }


    // Update is called once per frame
    void Update()
    { 
        if (Vector3.Dot(transform.up, Vector3.up) < 0) Pour();
        else
        {
            cork.SetActive(true);
            particle1.SetActive(false);
            particle2.SetActive(false);
            if (idealParent != null)
                Debug.Log(idealParent.rotation);
            if (!hasBeenAlt && idealParent != null)
                transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);
        }

    }

    protected virtual void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);

        cork.SetActive(false);
        particle1.SetActive(true);
        particle2.SetActive(true);

        foreach (RaycastHit hit in pouredOn)
        {
            if (hit.transform.gameObject.GetComponent<SlabManager>() != null)
                hit.transform.gameObject.GetComponent<SlabManager>().ChangeLiquid(LiquidColour, LiquidKey);

            else if (hit.transform.gameObject.GetComponent<Beaker>() != null)
                hit.transform.gameObject.GetComponent<Beaker>().AddLiquid(LiquidKey);
        }
    }

    public override void PickedUp()
    {
        transform.rotation = Quaternion.LookRotation(transform.parent.forward, transform.parent.up);
        base.PickedUp();
    }

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        hasBeenAlt = !hasBeenAlt;

        if(hasBeenAlt)
        {
            transform.Rotate(Vector3.forward, -120);
            aS.loop = true;
            aS.clip = pourSounds[Random.Range(0, pourSounds.Length)];
            aS.Play();
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);
            aS.loop = false;
            aS.Stop();
            aS.PlayOneShot(pourEnd);
        }

    }

    public override void Dropped()
    {
        cork.SetActive(true);
        particle1.SetActive(false);
        particle2.SetActive(false);

        aS.loop = false;
        aS.Stop();

        transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);

        base.Dropped();
    }

    public void Respawn()
    {
        cork.SetActive(true);
        particle1.SetActive(false);
        particle2.SetActive(false);
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

}
