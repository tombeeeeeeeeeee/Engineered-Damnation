using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // this gameobject should be added to the first scene that is loaded
    // and shouldn't be created again at any point after

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