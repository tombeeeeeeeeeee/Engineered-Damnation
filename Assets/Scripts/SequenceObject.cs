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

    protected virtual void Update()
    {
        timeInOperation += Time.deltaTime;
        inSequence = timeInOperation < lengthOfOperation;
        if (!inSequence)
            End();
    }

    /// <summary>
    /// Begin sequence
    /// </summary>
    /// <param name="decision">A bool for decision making</param>
    public virtual void Begin(bool decision)
    {
        this.decision = decision;

        if (!inSequence)
        {
            inSequence = true;
            gameObject.SetActive(true);
            timeInOperation = 0;
        }
    }

    /// <summary>
    /// End sequence.
    /// </summary>
    public virtual void End()
    { 
        if(nextInSequence != null)
        {
            timeInOperation = 0;
            nextInSequence.Begin(decision);
        }
    }
}
