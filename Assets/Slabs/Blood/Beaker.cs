using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Beaker : Potion
{
    [SerializeField] float pourRate;
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

            float fill = liquidLevel == 1 ? -shallowDepth : deepDepth;
            liquidMesh.material.SetFloat("_Fill", fill);
            Debug.Log(fill);

            aS.PlayOneShot(fillSounds[Random.Range(0, fillSounds.Length)]);
        }
    }

    protected override void Pour()
    {
        float fill = liquidMesh.material.GetFloat("_Fill");
        fill -= pourRate * Gameplay.deltaTime;
        liquidMesh.material.SetFloat("_Fill", fill);

        transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);
        pourAngle = Mathf.Acos(liquidMesh.material.GetFloat("_Fill")/2 + 0.5f) * 180 / Mathf.PI;
        transform.Rotate(-Vector3.forward, pourAngle);


        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        particle1.SetActive(true);
        particle2.SetActive(true);

        foreach (RaycastHit hit in pouredOn)
        {
            SlabManager slab = hit.transform.gameObject.GetComponent<SlabManager>();
            if ( slab != null && slab.getInner() != 0 )
                StartCoroutine(SlabColourChange(hit.transform.gameObject.GetComponent<SlabManager>(), hit.distance));
        }

        if(liquidMesh.material.GetFloat("_Fill") <= -0.8f)
        {
            liquidLevel = 0;
            LiquidKey = 0;
            liquid.SetActive(false);
            hasBeenAlt = false;
        }
    }

    public override void AlternateInteraction(InputAction.CallbackContext context)
    {
        pourAngle = Mathf.Acos(liquidMesh.material.GetFloat("_Fill")/2 + 0.5f )*180/Mathf.PI;

        if (liquidLevel > 0)
            base.AlternateInteraction(context);
    }

    public override IEnumerator SlabColourChange(SlabManager slab, float distance)
    {
        yield return new WaitForSeconds(Mathf.Sqrt(6 * distance / 9.81f));
        if(LiquidKey != 0) slab.ChangeLiquid(LiquidColour, LiquidKey);

        base.AlternateInteraction(new InputAction.CallbackContext());
    }

    public override void Respawn()
    {
        base.Respawn();
        liquidLevel = 0;
        LiquidKey = 0;
        liquid.SetActive(false);
    }
}
