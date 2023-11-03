using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabSpawner : MonoBehaviour
{
    public int cooldown;
    public float counter = 0;
    ToolSpawner spawner;

    private void Start()
    {
        spawner = GetComponent<ToolSpawner>();
        spawner.spawnTool();
    }

    private void Update()
    {
        counter += Time.deltaTime;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<SlabManager>() && counter >= cooldown)
        {
            spawner.spawnTool();
            counter = 0;
        }
    }
}
