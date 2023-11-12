using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : PickUp
{
    public uint LiquidKey = 0;
    public Color LiquidColour;
    [SerializeReference] protected Transform pourPosition;
    [SerializeField] protected float pourRadius;
    [SerializeField] protected float pourAngle;

    Vector3 spawnPosition;
    Quaternion spawnRotation;

    protected override void Start()
    {
        base.Start();
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
        foreach(RaycastHit hit in pouredOn)
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
        transform.rotation = Quaternion.AngleAxis(pourAngle, Vector3.one - transform.parent.up);
    }

    public override void Dropped()
    {
        transform.rotation = Quaternion.identity;
        base.Dropped();
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

}
