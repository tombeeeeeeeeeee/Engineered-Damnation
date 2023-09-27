using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // this gameobject is automatically instantiated by the Initialise GameObject,
    // which should be added to the first scene of the game.

    public static DataManager dataManager;
    public int money;
    public List<uint> demonsSuccess;
    public List<uint> demonsEscaped;

    void Awake()
    {
        DontDestroyOnLoad(this);
        dataManager = this; // other objects access this with DataManager.dataManager
    }
}