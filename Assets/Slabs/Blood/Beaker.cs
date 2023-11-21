
using UnityEngine;
using UnityEngine.InputSystem;

public class Beaker : Potion
{
    [SerializeField] Color[] colors;
    [SerializeField] int liquidLevel = 0;
    [SerializeField] GameObject[] liquidLevels;
    private int OldLiquidLevel = -1;

    public MeshRenderer[] liquidMesh;

    public GameObject mixPourPart;
    public AudioClip[] fillSounds;


    // Update is called once per frame

    void Awake() 
    {
        mixPourPart.SetActive(false);
    }
    void Update()
    {
        if(liquidLevel > 0 && Vector3.Dot(transform.up, Vector3.up) < 0) Pour();

        else
        {
            particle1.SetActive(false);
            particle2.SetActive(false);
        }

        if(OldLiquidLevel != liquidLevel)
        {
            mixPourPart.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = LiquidColour;
            for (int i = 0; i < 2; i++)
            {
                if (i < liquidLevel)
                {
                    liquidLevels[i].SetActive(true);
                    //liquidLevels[i].GetComponent<MeshRenderer>().material.color = LiquidColour;
                    liquidMesh[i].material.SetColor("_FresnelColor", LiquidColour);
                    liquidMesh[i].material.SetColor("_TopColor", LiquidColour);
                    liquidMesh[i].material.SetColor("_SideColor", LiquidColour);


                }
                else
                    liquidLevels[i].SetActive(false);
            }            

            OldLiquidLevel = liquidLevel;
        }
    }

    public void AddLiquid(uint liquidKey)
    {
        if (liquidLevel < 2 && liquidKey != LiquidKey)
        {
            
            LiquidKey += liquidKey;
            LiquidColour = colors[LiquidKey];
            liquidLevel++;
            aS.PlayOneShot(fillSounds[Random.Range(0, fillSounds.Length)]);
        }
    }

    protected override void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        mixPourPart.SetActive(true);
        foreach (RaycastHit hit in pouredOn)
        {
            SlabManager slab = hit.transform.gameObject.GetComponent<SlabManager>();
            if ( slab != null && slab.getInner() != 0 )
            {
                slab.ChangeLiquid(LiquidColour, LiquidKey);
                liquidLevel = 0;
                LiquidKey = 0;
                mixPourPart.SetActive(false);
            }
        }
    }

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        hasBeenAlt = !hasBeenAlt;

        if (hasBeenAlt)
        {
            transform.Rotate(Vector3.forward, -120);
            if(liquidLevel > 0)
            {
                aS.loop = true;
                aS.clip = pourSounds[Random.Range(0, pourSounds.Length)];
                aS.Play();
            }
        }
        else
        {
            transform.rotation = transform.rotation = Quaternion.LookRotation(-transform.parent.forward, transform.parent.up);
            if (liquidLevel > 0)
            {
                aS.loop = false;
                aS.Stop();
                aS.PlayOneShot(pourEnd);
            }
        }

    }

    public override void Dropped()
    {
        base.Dropped();
        mixPourPart.SetActive(false);
    }
}
