using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonListSpawner : MonoBehaviour
{

    public float ticketSpeed = 1;

    [SerializeField] OrderList ListHeaderPrefab;
    [SerializeField] Order childOrderPrefab;
    [SerializeField] Transform OrderSpawnTransform;
    
    public OrderList curList = null;
    private int ticketsOnList = 0;


    // Update is called once per frame
    void Update()
    {
        if (curList != null)
        {
            if((curList.transform.position - curList.attachPosition.position).magnitude * ticketsOnList > (OrderSpawnTransform.position - curList.transform.position).magnitude)
            {
                Debug.Log("Printing");
                curList.transform.position += curList.transform.up * ticketSpeed * Gameplay.deltaTime;
                    
            }
        }

        if(OrderSpawnTransform.GetComponentInChildren<OrderList>() == null)
        {
            curList = null;
            ticketsOnList = 0;
        }
    }

    public void AddToList(uint demonKey, string demonDescription)
    {
        if(curList == null)
        {
            curList = Instantiate(ListHeaderPrefab, OrderSpawnTransform, false);
            ticketsOnList++;
        }

        curList.AddToList(demonKey, demonDescription, childOrderPrefab);
        ticketsOnList++;

        curList.ExpandGrabArea();

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

