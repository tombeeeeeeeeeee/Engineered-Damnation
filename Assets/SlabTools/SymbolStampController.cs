using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolStampController : MonoBehaviour
{
    public int SymbolIndex = 0;
    public bool FlippedSymbol;

    [SerializeField] Material[] Symbols;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendSymbol()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            SlabManager slab = hit.collider.gameObject.GetComponent<SlabManager>();
            if ( slab != null)
                slab.ChangeSymbol(Symbols[SymbolIndex], FlippedSymbol, (uint)SymbolIndex);
        }
    }
}
