
using UnityEngine;

public class BloodBeaker : BloodVial
{
    [SerializeField] Color[] colors;
    [SerializeField] int liquidLevel = 0;
    [SerializeField] GameObject[] liquidLevels;
    private int OldLiquidLevel = -1;


    // Update is called once per frame
    void Update()
    {
        if(liquidLevel > 0 && Vector3.Dot(transform.up, Vector3.up) < 0) Pour();

        if(OldLiquidLevel != liquidLevel)
        {
            for (int i = 0; i < liquidLevels.Length; i++)
            {
                if (i < liquidLevel)
                {
                    liquidLevels[i].SetActive(true);
                    liquidLevels[i].GetComponent<MeshRenderer>().material.color = BloodColour;
                }
                else liquidLevels[i].SetActive(false);
            }

            OldLiquidLevel = liquidLevel;
        }
    }

    public void AddBlood(uint bloodKey)
    {
        if (liquidLevel < 2 && bloodKey != BloodKey)
        {
            BloodKey += bloodKey;
            BloodColour = colors[BloodKey];
            liquidLevel++;
        }
    }

    protected override void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        foreach (RaycastHit hit in pouredOn)
        {
            if (hit.rigidbody.gameObject.GetComponent<SlabManager>() != null)
            {
                hit.rigidbody.gameObject.GetComponent<SlabManager>().ChangeBlood(BloodColour, BloodKey);
                liquidLevel = 0;
                BloodKey = 0;
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
    }
}
