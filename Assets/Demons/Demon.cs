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

    public void Colour(Color colour)
    {
        Gameplay.ChildrenMaterialColour(gameObject, colour);
    }

    public void Summon(Vector3 summonLocation)
    {
        Debug.Log("summoned onscreen");
        transform.position = summonLocation + spawnOffset;
        
        if(animator)
            animator.Play(animationName);
    }

    public void Vanish()
    {
        Debug.Log("vanished offscreen");
        transform.position = offscreenPosition;
    }
}
