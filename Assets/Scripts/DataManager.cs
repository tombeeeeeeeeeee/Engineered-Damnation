using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Descriptor
{
    Horned = 10,
    Wings = 20,
    Headless = 30,
    NonPlanar = 40,
    Cloaked = 11,
    Chained = 21,
    Feral = 31,
    Engulfed = 41
}

public class DataManager : MonoBehaviour
{
    // this gameobject is automatically instantiated by the Initialise GameObject,
    // which should be added to the first scene of the game.

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