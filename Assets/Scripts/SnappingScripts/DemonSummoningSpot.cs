using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSummoningSpot : SnapSlab
{
    [SerializeField] SystemManager sysManager;
    [SerializeField] Transform DemonSummonTransform;
    [SerializeField] DemonPrefabKeyCombo[] demonPrefabs;
    [SerializeField] PotionPrefabKeyCombo[] potionPrefabs;
    [SerializeField] float slabToSpotPercentageOfTravel;
    [SerializeField] float SummonDuration;
    private float SummonFinishTime = 0;
    private bool movingtoSummonSpot;
    private GameObject currDemon = null;

    protected override void Update()
    {
        SummonFinishTime = (moving || movingtoSummonSpot) ? Time.time + 1 : SummonFinishTime;

        base.Update();

        if(movingtoSummonSpot)
        {
            ExpectedObject.transform.position = Vector3.Lerp(ExpectedObject.transform.position, DemonSummonTransform.position, slabToSpotPercentageOfTravel);
            if((DemonSummonTransform.position - ExpectedObject.transform.position).magnitude < 0.01f)
            {
                movingtoSummonSpot = false;
                SummonFinishTime = Time.time + SummonDuration; 
                SummonDemon();
            }
        }
        else if (Time.time < SummonFinishTime)
        {
            FinishSummon();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == ExpectedObject)
        {
            //Stop moving the object to the correct spot
            moving = false;
            movingtoSummonSpot = true;

            //if the object came from a player, take it away from them.
            if (pickupScript.heldObj == other.gameObject)
                pickupScript.DropObject();

            //put the object in the right spot.
            other.transform.rotation = transform.rotation;
            other.transform.position = transform.position;
            other.transform.SetParent(transform);
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
            for(int i = 0; i < demonPrefabs.Length; i++)
            {
                if (demonPrefabs[i].Key == slab.getType())
                    demonIndex = i;
            }
            for (int i = 0; i < potionPrefabs.Length; i++)
            {
                if (potionPrefabs[i].Key == slab.getBlood())
                    colourIndex = i;
            }
        }

        currDemon = Instantiate(demonPrefabs[demonIndex].Demon, DemonSummonTransform, false);
        currDemon.gameObject.GetComponent<MeshRenderer>().material.color = potionPrefabs[colourIndex].color;
    }

    
    private void FinishSummon()
    {
        Destroy(currDemon);
        Destroy(ExpectedObject);
    }

}

[Serializable]
public struct DemonPrefabKeyCombo
{
    public GameObject Demon;
    public uint Key;
}

[Serializable]
public struct PotionPrefabKeyCombo
{
    public Color color;
    public uint Key;
}

