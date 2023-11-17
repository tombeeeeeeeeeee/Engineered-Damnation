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
    public GameObject part1;
    public GameObject part2;

    public AudioClip[] pourSounds;

    Vector3 spawnPosition;
    Quaternion spawnRotation;

    protected override void Start()
    {
        base.Start();
        cork.SetActive(true);
        part1.SetActive(false);
        part2.SetActive(false);
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    { 
        if (Vector3.Dot(transform.up, Vector3.up) < 0) Pour();
    }

    protected virtual void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);

        cork.SetActive(false);
        part1.SetActive(true);
        part2.SetActive(true);

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
        base.PickedUp();

    }

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        if(hasBeenAlt)
        {
            transform.Rotate(transform.parent.forward, 120);
            aS.loop = true;
            aS.clip = pourSounds[Random.Range(0, pourSounds.Length)];
            aS.Play();
        }
        else
        {
            aS.loop = false;
            aS.Stop();
            transform.rotation = Quaternion.identity;
        }

        hasBeenAlt = !hasBeenAlt;
    }

    public override void Dropped()
    {
        cork.SetActive(true);
        part1.SetActive(false);
        part2.SetActive(false);

        aS.loop = false;
        aS.Stop();

        transform.rotation = Quaternion.identity;
        base.Dropped();
    }

    public void Respawn()
    {
        cork.SetActive(true);
        part1.SetActive(false);
        part2.SetActive(false);
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

}
