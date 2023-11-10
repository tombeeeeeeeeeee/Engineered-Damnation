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
    
    private OrderList curChildList = null;
    private int ticketsOnList = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curChildList != null)
        {
            if(curChildList.transform.localScale.y * ticketsOnList + 0.5f > curChildList.transform.localPosition.y)
                curChildList.MoveUp(ticketSpeed * Time.deltaTime);
        }

        if(OrderSpawnTransform.GetComponentInChildren<OrderList>() == null)
        {
            curChildList = null;
            ticketsOnList = 0;
        }
    }

    public void AddToList(uint demonKey, string demonDescription)
    {
        if(curChildList == null)
        {
            curChildList = Instantiate(ListHeaderPrefab, OrderSpawnTransform, false);
            ticketsOnList++;
        }

        curChildList.AddToList(demonKey, demonDescription, childOrderPrefab);
        ticketsOnList++;

        curChildList.ExpandGrabArea();

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

