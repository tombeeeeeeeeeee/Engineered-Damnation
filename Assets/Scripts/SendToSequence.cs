using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToSequence : SequenceObject
{
    public GameObject movingObject;
    public float speed;
    public Transform destination;

    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence)
        {
            lengthOfOperation += Time.deltaTime;
            movingObject.transform.LookAt(destination);
            movingObject.transform.position += Vector3.ClampMagnitude(movingObject.transform.forward*Time.deltaTime * speed, (destination.position - movingObject.transform.position).magnitude);
            if (movingObject.transform.position == destination.position)
            {
                inSequence = false;
                End();
            }
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence) movingObject.transform.LookAt(destination);
    }

    public override void End()
    {
        if(nextInSequence.GetComponent<SpinSequence>() != null) 
            nextInSequence.GetComponent<SpinSequence>().spinningObject = movingObject;
        base.End();
    }
}
