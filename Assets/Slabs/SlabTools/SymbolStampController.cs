using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    public int CircleIndex = 0;
    public int SymbolIndex = 0;

    public bool FlippedSymbol;
    [SerializeField] float pressingY;

    [SerializeField] Material[] Circles;
    [SerializeField] Material[] Symbols;
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
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(1, 1.5f, 1), -transform.up);
<<<<<<< Updated upstream
        
=======
>>>>>>> Stashed changes
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<SlabManager>())
                slab = hit.collider.gameObject.GetComponent<SlabManager>();
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
            //if(SymbolIndex != 0 || CircleIndex != 0)
                slab.ChangeBlood(new Color(0, 0, 0, 50), 0);
        }
    }
}
