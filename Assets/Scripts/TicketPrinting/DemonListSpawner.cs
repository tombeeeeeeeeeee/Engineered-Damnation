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

    [SerializeField] AudioClip tornSound;
    [SerializeField] AudioClip printingSound;
    
    public OrderList curList = null;
    private int ticketsOnList = 0;
    private AudioSource aS;
    private bool torn = true;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OrderSpawnTransform.GetComponentInChildren<OrderList>() == null)
        {
            curList = null;
            ticketsOnList = 0;

            if (!torn)
            {
                torn = true;
                aS.loop = false;
                if (Gameplay.active) aS.PlayOneShot(tornSound);
            }
        }

        if (curList != null)
        {
            if ((curList.transform.position - curList.attachPosition.position).magnitude * ticketsOnList
                > (OrderSpawnTransform.position - curList.transform.position).magnitude)
            {
                curList.transform.position += curList.transform.up * ticketSpeed * Gameplay.deltaTime;
                if (!aS.isPlaying && Gameplay.active) aS.Play();
                aS.loop = true;
            }
            else
            {
                aS.loop = false;
            }
        }
    }

    public void AddToList(uint demonKey, string demonDescription)
    {
        torn = false;

        if (curList == null)
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

