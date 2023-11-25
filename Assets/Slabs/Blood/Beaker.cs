
using UnityEngine;
using UnityEngine.InputSystem;

public class Beaker : Potion
{
    [SerializeField] float shallowPourAngle;
    [SerializeField] float deepPourAngle;
    [SerializeField] float shallowDepth;
    [SerializeField] float deepDepth;
    [SerializeField] Color[] colors;
    [SerializeField] int liquidLevel = 0;
    [SerializeField] GameObject liquid;
    [SerializeField] MeshRenderer liquidMesh;

    public AudioClip[] fillSounds;
    

    // Update is called once per frame

    void Awake() 
    {
        liquid.SetActive(false);
    }

    void Update()
    {
        if(hasBeenAlt && liquidLevel > 0) Pour();

        else
        {
            particle1.SetActive(false);
            particle2.SetActive(false);

            if (!hasBeenAlt && idealParent != null)
                transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);
        }
    }

    public void AddLiquid(uint liquidKey)
    {
        if (liquidLevel < 2 && liquidKey != LiquidKey)
        {
            LiquidKey += liquidKey;
            LiquidColour = colors[LiquidKey];
            liquidLevel++;

            liquid.SetActive(true);

            liquidMesh.material.SetColor("_FresnelColor", LiquidColour);
            liquidMesh.material.SetColor("_TopColor", LiquidColour);
            liquidMesh.material.SetColor("_SideColor", LiquidColour);

            particle1.GetComponent<Renderer>().material.color = LiquidColour;
            particle2.GetComponent<Renderer>().material.color = LiquidColour;

            float fill = liquidLevel == 1 ? shallowDepth : deepDepth;
            liquidMesh.material.SetFloat("_Fill", fill);

            aS.PlayOneShot(fillSounds[Random.Range(0, fillSounds.Length)]);
        }
    }

    protected override void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        particle1.SetActive(true);
        particle2.SetActive(true);

        foreach (RaycastHit hit in pouredOn)
        {
            SlabManager slab = hit.transform.gameObject.GetComponent<SlabManager>();
            if ( slab != null && slab.getInner() != 0 )
            {
                liquidLevel = 0;
                LiquidKey = 0;
                StartCoroutine(SlabColourChange(hit.transform.gameObject.GetComponent<SlabManager>(), LiquidColour, LiquidKey, hit.distance));
            }
        }
    }

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        pourAngle = liquidLevel > 1 ? deepPourAngle : shallowPourAngle;

        if (liquidLevel > 0)
            base.AlternateInteraction(context);
    }
}
