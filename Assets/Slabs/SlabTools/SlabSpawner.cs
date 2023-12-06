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
    private int slabCount = 0;

    private void Start()
    {
       toolSoundSpot = spawnLocation.gameObject.GetComponent<AudioSource>();
       SpawnTool();
    }

    private void Update()
    {
        //If no objectToDestroy spawning sound is playing while a objectToDestroy is spawning. play it
        if(queued && !toolSoundSpot.isPlaying) 
        {
            toolSoundSpot.PlayOneShot(SlabDroppingSounds[Random.Range(0,SlabDroppingSounds.Length)]);   
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SlabManager>() != null)
            slabCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SlabManager>() != null)
        {
            slabCount--;
            if (!queued && slabCount == 0)
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
        if(Gameplay.gameplayActive) toolSoundSpot.PlayOneShot(SlabSpawnSound);

        base.SpawnTool();
    }
}
