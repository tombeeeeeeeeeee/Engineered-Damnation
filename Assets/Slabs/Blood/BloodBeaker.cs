using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodBeaker : BloodVial
{
    [SerializeField] Color[] colors;
    [SerializeField] int liquidLevel = 0;
    [SerializeField] GameObject[] liquidLevels;
    [SerializeField] float cooldown = 0.2f;

    private float CooldownDone = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(liquidLevel > 0 && Vector3.Dot(transform.up, Vector3.up) < 0) Pour();

        if (liquidLevel == 0) foreach (GameObject level in liquidLevels) level.SetActive(false);

        else if (liquidLevel == 1) liquidLevels[0].SetActive(true);
        else if (liquidLevel == 2) liquidLevels[2].SetActive(true);

    }

    public void AddBlood(uint bloodKey)
    {
        if (liquidLevel < 2 && Time.time > CooldownDone)
        {
            BloodKey += bloodKey;
            BloodColour = colors[bloodKey];
            CooldownDone = Time.time + cooldown;
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
}
