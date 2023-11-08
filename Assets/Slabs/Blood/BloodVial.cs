using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVial : PickUp
{
    public uint BloodKey = 0;
    public Color BloodColour;
    [SerializeReference] protected Transform pourPosition;
    [SerializeField] protected float pourRadius;
    [SerializeField] protected float pourAngle;


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
            if (hit.rigidbody.gameObject.GetComponent<SlabManager>() != null)
                hit.rigidbody.gameObject.GetComponent<SlabManager>().ChangeBlood(BloodColour, BloodKey);

            else if (hit.rigidbody.gameObject.GetComponent<BloodBeaker>() != null)
                hit.rigidbody.gameObject.GetComponent<BloodBeaker>().AddBlood(BloodKey);
        }
    }

    public override void PickedUp()
    {
        base.PickedUp();
        transform.rotation = Quaternion.AngleAxis(pourAngle, transform.right);
    }

    public override void Dropped()
    {
        transform.rotation = Quaternion.identity;
        base.Dropped();
    }

}
