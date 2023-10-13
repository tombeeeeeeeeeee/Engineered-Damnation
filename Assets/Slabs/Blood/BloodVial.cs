using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVial : MonoBehaviour
{
    public uint BloodKey = 0;
    public Color BloodColour;
    [SerializeReference] Transform pourPosition;
    [SerializeField] float pourRadius;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.up) < 0) Pour();
    }

    private void Pour()
    {
        RaycastHit[] pouredOn;
        pouredOn = Physics.SphereCastAll(pourPosition.position, pourRadius, -Vector3.up, 1);
        foreach(RaycastHit hit in pouredOn)
        {
            if(hit.rigidbody.gameObject.GetComponent<SlabManager>() != null)
            {
                hit.rigidbody.gameObject.GetComponent<SlabManager>().ChangeBlood(BloodColour, BloodKey);
            }
        }
    }

}
