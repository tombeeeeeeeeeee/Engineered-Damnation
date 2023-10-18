using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    public int CircleIndex = 0;
    public int SymbolIndex = 0;

    public bool FlippedSymbol;
    [SerializeField] float pressingY = 0;
    private float startingY;
    [SerializeField] Material[] Circles;
    [SerializeField] Material[] Symbols;
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if ((pressingY + startingY) == transform.position.y)
            PressStamp();
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
            if (SymbolIndex != 0)
                slab.ChangeSymbol(Symbols[SymbolIndex], FlippedSymbol, (uint)SymbolIndex);

            //Check if there is a circle stencil
            if (CircleIndex != 0)
                slab.ChangeCircle(Circles[CircleIndex], (uint)CircleIndex);

            //Give a faint imprint of the press onto the slab
                slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
