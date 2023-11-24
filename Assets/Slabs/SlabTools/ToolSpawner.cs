using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawner : MonoBehaviour
{
    public GameObject[] toolCollection;
    [SerializeReference] protected Transform spawnLocation;

    public virtual void SpawnTool() { Instantiate(GetToolFromCollection(), spawnLocation); }

    public GameObject GetToolFromCollection() { return toolCollection[Random.Range(0,toolCollection.Length - 1)]; }
}
