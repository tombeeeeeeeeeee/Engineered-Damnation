using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : OrderList
{
    public uint demonKey;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image checkedOffArt;
    

    public void Initialise(uint demonKey, string demonDescription)
    {
        this.demonKey = demonKey;
        this.text.text = demonDescription;
        checkedOffArt.enabled = false ;
    }

    /// <summary>
    /// Check Off a Demon from a todo list
    /// </summary>
    /// <param name="demonKey"></param>
    /// <returns>True when the key has been found.</returns>
    public override bool CheckOffDemon(uint demonKey)
    {
        //If the keys match, and this demon hasn't been checked off
        if(demonKey == this.demonKey && !checkedOffArt.enabled)
        {
            //check off the demon, and stop the recursion
            checkedOffArt.enabled = true;
            return true;
        }
        else  //continue down the list
           return base.CheckOffDemon(demonKey);
    }
}
