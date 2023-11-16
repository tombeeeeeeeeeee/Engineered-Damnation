using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SequenceObject : MonoBehaviour
{
    public bool inSequence = false; //Is the sequence running?
    [SerializeField] protected SequenceObject nextInSequence; //The next sequence node, if null then its the end of the node.
    [SerializeField] protected float lengthOfOperation; //Length that the sequence runs for.
    protected float timeInOperation;  //counting the length of the sequence.
    protected bool decision;
    /// <summary>
    /// Begin sequence
    /// </summary>
    /// <param name="decision">A bool for decision making</param>
    public abstract void Begin(bool decision);

    /// <summary>
    /// End sequence.
    /// </summary>
    public abstract void End();
}
