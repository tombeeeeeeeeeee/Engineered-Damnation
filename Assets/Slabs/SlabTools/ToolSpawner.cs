using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawner : MonoBehaviour
{
    public GameObject[] toolCollection;
    [SerializeReference] Transform spawnLocation;

    public void spawnTool() { Instantiate(getToolFromCollection(), spawnLocation); }

    public GameObject getToolFromCollection() { return toolCollection[Random.Range(0,toolCollection.Length - 1)]; }
}
