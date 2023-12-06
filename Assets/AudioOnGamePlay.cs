using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnGamePlay : MonoBehaviour
{
    void Update()
    {
        if (Gameplay.active && !GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}
