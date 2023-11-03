using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonListSpawner : MonoBehaviour
{
    public WordKeyCombo[] DemonTypes;
    public WordKeyCombo[] DemonAdjectives;

    public float ticketSpeed = 1;


    [SerializeField] OrderList ListHeaderPrefab;
    [SerializeField] Order childOrderPrefab;
    [SerializeField] Transform OrderSpawnTransform;
    
    private OrderList curChildList = null;
    private int ticketsOnList = 0;
    [SerializeField] float ticketMoveDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curChildList != null)
        {
            if(ticketMoveDistance * ticketsOnList > curChildList.transform.position.y - OrderSpawnTransform.position.y)
                curChildList.MoveUp(ticketSpeed);
        }

        if(OrderSpawnTransform.GetComponentInChildren<OrderList>() == null)
        {
            curChildList = null;
            ticketsOnList = 0;
        }
    }

    public void AddToList(uint demonKey)
    {
        if(curChildList == null)
        {
            curChildList = Instantiate(ListHeaderPrefab, OrderSpawnTransform, false);
            ticketsOnList++;
        }

        string demonDescription = "";

        foreach (WordKeyCombo adjective in DemonAdjectives)
        {
            if (adjective.Key == demonKey % 10)
                demonDescription += adjective.Descriptor;
        }

        demonDescription += " ";

        foreach (WordKeyCombo demon in DemonTypes)
        {
            if (demon.Key == demonKey / 10)
                demonDescription += demon.Descriptor;
        }
       

        curChildList.AddToList(demonKey, demonDescription, childOrderPrefab);
        ticketsOnList++;

    }

    public bool CheckOffDemon(uint demonKey)
    {
        bool foundDemon = false;

        OrderList[] OrderLists = FindObjectsOfType<OrderList>();
        foreach(OrderList orderList in OrderLists)
        {
            foundDemon = orderList.CheckOffDemon(demonKey);
            if (foundDemon) return foundDemon;
        }
        return foundDemon;
    }

}
[Serializable]
public struct WordKeyCombo
{
    public uint Key;
    public string Descriptor;
}
