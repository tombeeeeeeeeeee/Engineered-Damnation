using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // this gameobject is automatically instantiated by the Initialise GameObject,
    // which should be added to the first scene of the game.

    System.Random rand = new System.Random();

    public static DataManager dataManager;
    public int money;
    public int day;
    public List<uint> orders;
    public List<uint> demonsSuccess;
    public List<uint> demonsEscaped;

    void Awake()
    {
        day = 1;
        DontDestroyOnLoad(this);
        dataManager = this; // other objects access this with DataManager.dataManager
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            orders.Add(RandomDemon());
        }

    }

    uint RandomDemon()
    {
        uint demonKey = 0;

        demonKey += (uint)rand.Next(1, 4);              // outer circle
        demonKey += (uint)rand.Next(0, 1) * 10;         // flipped
        demonKey += (uint)rand.Next(1, 4) * 100;        // symbol
        demonKey += (uint)rand.Next(0, 1) * 1000;       // candles
        demonKey += (uint)rand.Next(1, 4) * 10000;      // slab
        demonKey += (uint)rand.Next(1, 4) * 100000;     // blood

        return demonKey;
    }

    void SubmitOrder(uint demonKey)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (demonKey == orders[i])
            {
                // order succesfully completed
                demonsSuccess.Add(demonKey);
                orders.RemoveAt(i);
                Debug.Log("correct");
                return;
            }
            // no matching order
            Debug.Log("wrong");
            demonsEscaped.Add(demonKey);
        }
    }
}