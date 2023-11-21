using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSummoningSpot : SnapSlab
{
    [SerializeField] SystemManager sysManager;
    [SerializeField] SendToSequence nextInSequence;
    private GameObject demonToSummon;
    private int colourIndex = 0;

    protected override void Update()
    {
        base.Update();

    }

    public override void OnTriggerEnter(Collider other)
    {
        OnTriggerStay(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (SnapType(other.gameObject) && ExpectedObject == null)
        {
            ExpectedObject = other.gameObject;
        }

        if (other.gameObject == ExpectedObject && !moving)
        {
            //Stop moving the object to the correct spot
            moving = true;

            other.GetComponent<Rigidbody>().isKinematic = true;

            //if the object came from a player, take it away from them.
            if (pickupScript.heldObj == other.gameObject)
                pickupScript.DropObject();

            //put the object in the right spot.
            nextInSequence.movingObject = other.gameObject;
            nextInSequence.Begin(SummonDemon());
        }
    }


    private bool SummonDemon()
    {
        SlabManager slab = ExpectedObject.GetComponent<SlabManager>();
        int demonIndex = 0;

        if(slab && slab.getLiquid() != 0)
        {
            sysManager.SummonedDemon(slab.DemonKey);
            for(int i = 0; i < sysManager.DemonTypes.Length; i++)
            {
                if (sysManager.DemonTypes[i].KeyIndex == slab.getType())
                    demonIndex = i;
            }
            for (int i = 0; i < sysManager.LiquidTypes.Length; i++)
            {
                if (sysManager.LiquidTypes[i].KeyIndex == slab.getLiquid())
                    colourIndex = i;
            }
        }

        demonToSummon = sysManager.DemonTypes[demonIndex].Demon;
        Demon demon = demonToSummon.gameObject.GetComponent<Demon>(); 
        if(demon)
            demon.Colour(sysManager.LiquidTypes[colourIndex].color);

        return demonToSummon.gameObject != null;
    }

}

