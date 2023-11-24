using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqeunceStarter : MonoBehaviour
{
    [SerializeField] SequenceObject SequenceToStart;
    [SerializeField] bool SequenceBool;

    // Start is called before the first frame update
    void Start()
    {
        SequenceToStart.Begin(SequenceBool);
    }
}
