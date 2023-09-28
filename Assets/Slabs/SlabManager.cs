using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabManager : MonoBehaviour
{
    //This key catalogues the type of demon the slab will summon
    public uint DemonKey = 000000;

    public void ChangeBlood(Color color, uint BloodKey)
    {
        MeshRenderer[] SlabArt = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer art in SlabArt)
            if(art != GetComponent<MeshRenderer>())
                art.material.color = color;
        DemonKeyUpdate(1, BloodKey);
    }
    public void ChangeStoneMaterial(Material mat, uint StoneKey)
    {
        //Change with new material
        GetComponent<MeshRenderer>().material = mat;
        DemonKeyUpdate(10, StoneKey);
    }

    public void ChangeCandleToggle(bool Candle)
    {
        transform.Find("Candle").gameObject.SetActive(Candle);
        DemonKeyUpdate(100, (uint)(Candle?1:0));
    }

    public void ChangeSymbol(Material mat, bool Flipped, uint SymbolKey)
    {
        foreach (Transform art in transform)
        {
            if(art.name.ToLower() == "inner symbol")
            {
                art.GetComponent<MeshRenderer>().material = mat;
                art.transform.localRotation = Quaternion.AngleAxis((Flipped ? 180 : 0), transform.up);
            }
        }
        DemonKeyUpdate(1000, SymbolKey);
        DemonKeyUpdate(10000, (uint)(Flipped ? 1 : 0));
    }

    public void ChangeCircle(Material mat, uint CircleKey)
    {
        foreach (Transform art in transform)
        {
            if (art.name.ToLower() == "outer circle")
            {
                art.GetComponent<MeshRenderer>().material = mat;
            }
        }
        DemonKeyUpdate(100000, CircleKey);
    }

    public void DemonKeyUpdate(uint KeySlotIndex, uint KeyValue)
    {
        //Determine the value of the current key in the slot.
        uint KeySlotCurrentValue = (DemonKey/KeySlotIndex) % 10;
        
        //If the value in the keyslot is not 0, then delete it.
        if (KeySlotCurrentValue > 0) 
            DemonKey -= KeySlotCurrentValue * KeySlotIndex;

        //Make value at the Keyslot Index equal to the new value.
        DemonKey += KeyValue * KeySlotIndex;
    }

    public void CleanSlate()
    {
        DemonKey = 0;
        foreach (Transform art in transform)
        {
            art.GetComponent<MeshRenderer>().material = null;
        }

    }
}
