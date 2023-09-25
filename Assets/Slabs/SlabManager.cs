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
        Material[] SlabArt = GetComponentsInChildren<Material>();
        foreach (Material art in SlabArt)
            art.color = color;
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
        GameObject[] SlabArt = GetComponentsInChildren<GameObject>();
        foreach (GameObject art in SlabArt)
        {
            if(art.name.ToLower() == "inner symbol")
            {
                art.GetComponent<MeshRenderer>().material = mat;
                art.transform.localRotation = Quaternion.AngleAxis(180 * (Flipped ? 1 : 0), transform.up);
            }
        }
        DemonKeyUpdate(1000, SymbolKey);
        DemonKeyUpdate(10000, (uint)(Flipped ? 1 : 0));
    }

    public void ChangeCircle(Material mat, uint CircleKey)
    {
        GameObject[] SlabArt = GetComponentsInChildren<GameObject>();
        foreach (GameObject art in SlabArt)
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
        GameObject[] SlabArt = GetComponentsInChildren<GameObject>();
        foreach (GameObject art in SlabArt)
        {
            art.GetComponent<MeshRenderer>().material = null;
        }

    }
}
