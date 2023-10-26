using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise : MonoBehaviour
{
    public DataManager dataManagerPrefab;
    void Awake()
    {
        // creates a DataManager if one doesn't already exists
        // this Initialise object should be put in the first scene (main menu probably)
        if (DataManager.dataManager == null)
        {
            Instantiate(dataManagerPrefab);
        }
    }
}