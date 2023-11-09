using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    [SerializeField] RingManager innerSymbol;
    [SerializeField] RingManager outerSymbol;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PressStamp()
    {
        SlabManager slab = null;

        Debug.Log(gameObject.name);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.GetComponentInChildren<SlabManager>())
                slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
        }

        if (slab != null)
        {
            slab.ChangeInner(innerSymbol.symbol, (uint)innerSymbol.symbolIndex);

            slab.ChangeOuter(outerSymbol.symbol, (uint)outerSymbol.symbolIndex);

            //Give a faint imprint of the press onto the slab
            slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
