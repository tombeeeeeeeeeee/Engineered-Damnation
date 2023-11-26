using UnityEngine;

public class DemonSummoningSpot : SnapSlab
{
    [SerializeField] SystemManager sysManager;
    [SerializeField] SendToSequence nextInSequence;
    [HideInInspector] public GameObject demonToSummon;
    [HideInInspector] public Color colourToSummon;
    [HideInInspector] public Color shaderColourToSummon;
    private int colourIndex = 0;
    [HideInInspector] public bool summoning = false;
    [SerializeField] AudioClip[] Whispers;
    public override void OnTriggerEnter(Collider other)
    {
        if (pickupScript.heldObj == other.gameObject) return;

        if (SnapType(other.gameObject) && ExpectedObject == null)
            ExpectedObject = other.gameObject;

        moving = !(other.gameObject == ExpectedObject && !summoning);

    }

    public void OnTriggerStay(Collider other)
    {
        if (pickupScript.heldObj == other.gameObject) return;

        if (SnapType(other.gameObject) && ExpectedObject == null)
            ExpectedObject = other.gameObject;

        if (other.gameObject == ExpectedObject && !summoning)
        {
            //Stop moving the object to the correct spot
            summoning = true;

            //other.GetComponent<Rigidbody>().isKinematic = true;

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

        if (demonIndex != 0)
        {
            demonToSummon = sysManager.DemonTypes[demonIndex].Demon;
            colourToSummon = sysManager.LiquidTypes[colourIndex].color;
            shaderColourToSummon = sysManager.LiquidTypes[colourIndex].shaderColor;
        }
        else demonToSummon = null;

        GetComponent<AudioSource>().PlayOneShot(Whispers[Random.Range(0,Whispers.Length)]);
        return demonToSummon != null;
    }

}

