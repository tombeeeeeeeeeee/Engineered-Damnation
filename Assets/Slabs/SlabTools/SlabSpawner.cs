using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabSpawner : MonoBehaviour
{
    public int cooldown;
    int counter = 0;
    ToolSpawner spawner;

    private void Start()
    {
        spawner = GetComponent<ToolSpawner>();
        spawner.spawnTool();
    }

    private void FixedUpdate()
    {
        if (counter > 0)
        {
            counter--;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<SlabManager>() /*&& counter <= 0*/)
        {
            spawner.spawnTool();
            counter = cooldown;
        }
    }
}
