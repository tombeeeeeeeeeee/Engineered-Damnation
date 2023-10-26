using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    [SerializeField] RingManager innerSymbol;
    [SerializeField] RingManager outerSymbol;

<<<<<<< Updated upstream
    public bool FlippedSymbol;
    [SerializeField] float pressingY;

    [SerializeField] Material[] Circles;
    [SerializeField] Material[] Symbols;
=======
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressingY == transform.position.y)
            PressStamp();
    }


    public void PressStamp()
    {
        SlabManager slab = null;
<<<<<<< Updated upstream
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(1, 0.5f, 1), -transform.up);
        foreach(RaycastHit hit in hits)
=======
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
>>>>>>> Stashed changes
        {
            if (hit.collider.gameObject.GetComponent<SlabManager>())
                slab = hit.collider.gameObject.GetComponent<SlabManager>();
        }

        if (slab != null)
        {
<<<<<<< Updated upstream
            //Check if there is a symbol stencil
            if (SymbolIndex != 0)
                slab.ChangeSymbol(Symbols[SymbolIndex], FlippedSymbol, (uint)SymbolIndex);


            //Check if there is a circle stencil
            if (CircleIndex != 0)
                slab.ChangeCircle(Circles[CircleIndex], (uint)CircleIndex);
=======
            slab.ChangeInner(innerSymbol.symbol, (uint)innerSymbol.symbolIndex);

            slab.ChangeOuter(outerSymbol.symbol, (uint)outerSymbol.symbolIndex);
>>>>>>> Stashed changes

            //Give a faint imprint of the press onto the slab
            //if(SymbolIndex != 0 || CircleIndex != 0)
                slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
