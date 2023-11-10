using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabSpawner : ToolSpawner
{
    [SerializeField] float cooldownDuration = 0.2f;
    private bool queued = false;

    private void Start()
    {
       SpawnTool();
    }

    private void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SlabManager>() != null)
        {
            if (!queued)
            {
                queued = true;
                Invoke("SpawnTool", cooldownDuration);
            }
        }
    }


    public override void SpawnTool()
    {
        queued = false;
        base.SpawnTool();
    }
}
