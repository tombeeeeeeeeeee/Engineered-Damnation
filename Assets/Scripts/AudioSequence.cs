using UnityEngine;

public class AudioSequence : SequenceObject
{
    [SerializeField] AudioClip[] audioClips;

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            int index = Random.Range(0, audioClips.Length);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioClips[index]);
            lengthOfOperation = audioClips[index].length;
        }
    }
}
