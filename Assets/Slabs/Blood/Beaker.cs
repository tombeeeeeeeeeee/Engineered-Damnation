
using UnityEngine;

public class Beaker : Potion
{
    [SerializeField] Color[] colors;
    [SerializeField] int liquidLevel = 0;
    [SerializeField] GameObject[] liquidLevels;
    private int OldLiquidLevel = -1;

    public MeshRenderer[] liquidMesh;

    public GameObject mixPourPart;


    // Update is called once per frame

    void Awake() 
    {
        mixPourPart.SetActive(false);
    }
    void Update()
    {
        if(liquidLevel > 0 && Vector3.Dot(transform.up, Vector3.up) < 0) Pour();

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
        }
    }

    protected override void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        mixPourPart.SetActive(true);
        foreach (RaycastHit hit in pouredOn)
        {
            if (hit.transform.gameObject.GetComponent<SlabManager>() != null)
            {
                hit.transform.gameObject.GetComponent<SlabManager>().ChangeLiquid(LiquidColour, LiquidKey);
                liquidLevel = 0;
                LiquidKey = 0;
                mixPourPart.SetActive(false);
            }
        }
    }

    public override void PickedUp()
    {
        base.PickedUp();
    }

    public override void Dropped()
    {
        base.Dropped();
        mixPourPart.SetActive(false);
    }
}
