using UnityEngine;

public class SlabManager : MonoBehaviour
{
    //This key catalogues the type of demon the slab will summon
    public uint DemonKey = 000;
    private MeshRenderer[] meshRenderers;

    private void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void ChangeLiquid(Color color, uint liquidKey)
    {
        if (getOuter() != 0)
            meshRenderers[2].material.color = color;

        if(getInner() != 0)
            meshRenderers[1].material.color = color;

        if (getOuter() != 0 || getInner() != 0)
            DemonKeyUpdate(1, liquidKey);
    }


    public void ChangeInner(Material mat, uint SymbolKey)
    {
        meshRenderers[1].material = mat;
        DemonKeyUpdate(10, SymbolKey);
    }

    public void ChangeOuter(Material mat, uint CircleKey)
    {
        meshRenderers[2].material = mat;
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

    public int getType() { return (int)DemonKey / 10; } 
    public int getLocation() { return (int)DemonKey % 10; }

    public int getOuter() { return (int)DemonKey / 100; }
    public int getInner() { return ((int)DemonKey / 10) % 10; }
    public int getLiquid() { return (int)DemonKey % 10; }
}
