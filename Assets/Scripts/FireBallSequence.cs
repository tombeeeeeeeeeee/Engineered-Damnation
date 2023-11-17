using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class FireBallSequence : SequenceObject
{
    [SerializeField] Demon cat;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence)
        {
            transform.position += velocity * Time.deltaTime;
            base.Update();
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if (!inSequence)
            velocity = (cat.transform.position - transform.position)/lengthOfOperation;
    }

    public override void End()
    {
       base.End();
       gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        ExplosionChange explodee = other.gameObject.GetComponent<ExplosionChange>();
        if (explodee != null)
            explodee.Explode();
    }
}
