using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SlabManager : PickUp
{
    //This key catalogues the type of demon the objectToDestroy will summon
    public uint DemonKey = 000;
    private MeshRenderer[] meshRenderers;
    private float burnStrength = 0;
    [SerializeField] float burnDecreasePercent = 1;
    [SerializeField] GameObject[] burnParticles;
    [SerializeField] ParticleSystem smoke;

    protected override void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        base.Start();
    }


    protected override void Update()
    {
        if (!hasBeenAlt && idealParent != null)
            transform.rotation = Quaternion.LookRotation(idealParent.up, -idealParent.forward);
        burnStrength -= Gameplay.deltaTime * burnDecreasePercent/100;
        burnStrength = Mathf.Clamp(burnStrength, 0, 1);

        for(int i = 1; i < 3; i++)
            meshRenderers[i].material.SetFloat("_Burnin", burnStrength);
        Debug.Log(burnStrength);

        foreach(GameObject fire in burnParticles)
        {
            fire.gameObject.SetActive(burnStrength > 0);
            fire.transform.localScale = Vector3.one * burnStrength;
        }
    }


    public void AddToFlames(float increase)
    {
        burnStrength += increase * burnDecreasePercent / 100;
    }


    public void ChangeLiquid(Color color, uint liquidKey)
    {
        if (getOuter() != 0)
            meshRenderers[2].material.SetColor("_BaseColour", color);

        if (getInner() != 0)
            meshRenderers[1].material.SetColor("_BaseColour", color);

        if (getOuter() != 0 || getInner() != 0)
            DemonKeyUpdate(1, liquidKey);
    }


    public void ChangeInner(Material mat, uint SymbolKey)
    {
        meshRenderers[1].material = mat;
        DemonKeyUpdate(10, SymbolKey);
        burnStrength = 1.5f;
        smoke.gameObject.SetActive(true);
        smoke.Play();
    }

    public void ChangeOuter(Material mat, uint CircleKey)
    {
        meshRenderers[2].material = mat;
        DemonKeyUpdate(100, CircleKey);
        burnStrength = 1.5f;
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

    public int getType() { return (int)DemonKey / 10; } 
    public int getLocation() { return (int)DemonKey % 10; }

    public int getOuter() { return (int)DemonKey / 100; }
    public int getInner() { return ((int)DemonKey / 10) % 10; }
    public int getLiquid() { return (int)DemonKey % 10; }


}
