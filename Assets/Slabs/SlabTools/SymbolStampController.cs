using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    public int CircleIndex = 0;
    public int SymbolIndex = 0;

    [SerializeField] Material[] OuterSymbols;
    [SerializeField] Material[] InnerSymbols;
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
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.GetComponentInChildren<SlabManager>())
                slab = hit.collider.gameObject.GetComponentInChildren<SlabManager>();
        }

        if (slab != null)
        {
            //Check if there is a symbol stencil
            slab.ChangeInner(InnerSymbols[SymbolIndex], (uint)SymbolIndex);

            //Check if there is a circle stencil
            slab.ChangeOuter(OuterSymbols[CircleIndex], (uint)CircleIndex);

            //Give a faint imprint of the press onto the slab
            slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
