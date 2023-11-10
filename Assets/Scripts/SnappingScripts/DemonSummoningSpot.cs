using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSummoningSpot : SnapSlab
{
    [SerializeField] SystemManager sysManager;
    [SerializeField] Transform DemonSummonTransform;
    [SerializeField] float slabToSpotPercentageOfTravel;
    [SerializeField] float SummonDuration;
    private float SummonFinishTime = 0;
    [SerializeField] bool movingtoSummonSpot = false;
    private GameObject currDemon = null;

    protected override void Update()
    {
        SummonFinishTime = (moving || movingtoSummonSpot) ? Time.time + 1 : SummonFinishTime;

        base.Update();

        if(movingtoSummonSpot)
        {
            ExpectedObject.transform.position = Vector3.Lerp(ExpectedObject.transform.position, DemonSummonTransform.position, slabToSpotPercentageOfTravel * Time.deltaTime);
            if((DemonSummonTransform.position - ExpectedObject.transform.position).magnitude < 0.01f)
            {
                movingtoSummonSpot = false;
                SummonFinishTime = Time.time + SummonDuration; 
                SummonDemon();
            }
        }
        else if (Time.time > SummonFinishTime)
        {
            FinishSummon();
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        if(!movingtoSummonSpot)
        {
            if (SnapType(other.gameObject) && ExpectedObject == null)
            {
                ExpectedObject = other.gameObject;
            }

            if (other.gameObject == ExpectedObject)
            {
                //Stop moving the object to the correct spot
                moving = false;
                movingtoSummonSpot = true;

                other.GetComponent<Rigidbody>().isKinematic = true;

                //if the object came from a player, take it away from them.
                if (pickupScript.heldObj == other.gameObject)
                    pickupScript.DropObject();

                //put the object in the right spot.
                other.transform.rotation = transform.rotation;
                other.transform.position = transform.position;
                other.transform.SetParent(transform);
            }
        }
    }


    private void SummonDemon()
    {
        SlabManager slab = ExpectedObject.GetComponent<SlabManager>();
        int demonIndex = 0;
        int colourIndex = 0;
        if(slab && slab.getBlood() != 0)
        {
            sysManager.SummonedDemon(slab.DemonKey);
            for(int i = 0; i < sysManager.DemonTypes.Length; i++)
            {
                if (sysManager.DemonTypes[i].KeyIndex == slab.getType())
                    demonIndex = i;
            }
            for (int i = 0; i < sysManager.LiquidTypes.Length; i++)
            {
                if (sysManager.LiquidTypes[i].KeyIndex == slab.getBlood())
                    colourIndex = i;
            }
        }

        currDemon = Instantiate(sysManager.DemonTypes[demonIndex].Demon, DemonSummonTransform, false);
        currDemon.transform.position += Vector3.up *0.1f;
        Demon demon = currDemon.gameObject.GetComponent<Demon>(); 
        if(demon)
            demon.Colour(sysManager.LiquidTypes[colourIndex].color);
    }

    
    private void FinishSummon()
    {
        Destroy(currDemon);
        Destroy(ExpectedObject);
    }

}

