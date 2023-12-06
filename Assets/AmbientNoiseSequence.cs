using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoiseSequence : SequenceObject
{
    [SerializeField] AudioClip[] AmbientNoises;

    public override void Begin(bool decision)
    {
        transform.position = new Vector3(Random.value, Random.value, Random.value).normalized * Random.Range(3, 5);
        int index = Random.Range(0,AmbientNoises.Length);
        GetComponent<AudioSource>().PlayOneShot(AmbientNoises[index]);
        lengthOfOperation = AmbientNoises[index].length;
        base.Begin(decision);
    }

}
