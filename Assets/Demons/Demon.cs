using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] Vector3 spawnOffset = Vector3.zero;
    private Vector3 offscreenPosition;
    private Animator animator;
    [SerializeField] string animationName;


    private void Start()
    {
        animator = GetComponent<Animator>();
        offscreenPosition = transform.position;
    }

    /// <summary>
    /// Colours the demon with its shader 
    /// </summary>
    /// <param name="colour"></param>
    public void Colour(Color colour)
    {
        Gameplay.ChildrenMaterialColour(gameObject, colour);
    }

    /// <summary>
    /// Summons Demon to provided location, with offset
    /// </summary>
    /// <param name="summonLocation"></param>
    public void Summon(Vector3 summonLocation)
    {
        transform.position = summonLocation + spawnOffset;
        
        if(animator)
            animator.Play(animationName);
    }

    /// <summary>
    /// Sends demon back to where they started the scene.
    /// </summary>
    public void Vanish()
    {
        transform.position = offscreenPosition;
    }
}
