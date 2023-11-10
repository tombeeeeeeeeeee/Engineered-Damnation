using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SlabManager : MonoBehaviour
{
    //This key catalogues the type of demon the slab will summon
    public MeshRenderer meshRenderer;
    public uint DemonKey = 000;

    public void ChangeBlood(Color color, uint BloodKey)
    {
        Material[] materials = GetComponent<MeshRenderer>().materials;

        MeshRenderer[] SlabArt = GetComponentsInChildren<MeshRenderer>();
        if (getOuter() != 0)
            materials[2].color = color;

        if(getInner() != 0)
            materials[1].color = color;

        GetComponent<MeshRenderer>().materials = materials;

        if (getOuter() != 0 || getInner() != 0)
            DemonKeyUpdate(1, BloodKey);

    }


    public void ChangeInner(Material mat, uint SymbolKey)
    {
        Debug.Log("inner: " + SymbolKey);
        //Material[] materials = GetComponent<MeshRenderer>().materials;
        //materials[1] = mat;
        //GetComponent<MeshRenderer>().materials = materials;

        DemonKeyUpdate(10, SymbolKey);
    }

    public void ChangeOuter(Material mat, uint CircleKey)
    {
        Debug.Log("outer: " + CircleKey);
        Material[] materials = meshRenderer.materials;
        materials[2] = mat;
        meshRenderer.materials = materials;
        DemonKeyUpdate(100, CircleKey);
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

    public int getType() { return (int)DemonKey / 10; } 
    public int getLocation() { return (int)DemonKey % 10; }

    public int getOuter() { return (int)DemonKey / 100; }
    public int getInner() { return ((int)DemonKey / 10) % 10; }
    public int getBlood() { return (int)DemonKey % 10; }
}
