using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabSpawner : ToolSpawner
{
    [SerializeField] float cooldownDuration = 0.2f;
    private bool queued = false;
    [SerializeField] AudioClip SlabSpawnSound;
    [SerializeField] AudioClip[] SlabDroppingSounds;
    private AudioSource toolSoundSpot;

    private void Start()
    {
       toolSoundSpot = spawnLocation.gameObject.GetComponent<AudioSource>();
       SpawnTool();
    }

    private void Update()
    {
        //If no slab spawning sound is playing while a slab is spawning. play it
        if(queued && !toolSoundSpot.isPlaying) 
        {
            toolSoundSpot.PlayOneShot(SlabDroppingSounds[Random.Range(0,SlabDroppingSounds.Length)]);   
        }
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
        if(toolSoundSpot.isPlaying)
            toolSoundSpot.Stop();
        toolSoundSpot.PlayOneShot(SlabSpawnSound);
        base.SpawnTool();
    }
}
