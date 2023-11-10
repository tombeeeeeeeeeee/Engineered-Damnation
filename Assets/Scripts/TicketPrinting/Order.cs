using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : OrderList
{
    public uint demonKey;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image checkedOffArt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }
    public void Initialise(uint demonKey, string demonDescription)
    {
        this.demonKey = demonKey;
        this.text.text = demonDescription;
        checkedOffArt.enabled = false ;
    }


    public override bool CheckOffDemon(uint demonKey)
    {
        if(demonKey == this.demonKey)
        {
            checkedOffArt.enabled = true;
            return true;
        }
        else 
           return base.CheckOffDemon(demonKey);
    }
}
