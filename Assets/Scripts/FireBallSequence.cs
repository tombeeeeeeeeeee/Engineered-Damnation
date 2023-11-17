using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class FireBallSequence : SequenceObject
{
    [SerializeField] Demon cat;
    private float speed = 0;

    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence)
        {
            transform.position += (cat.transform.position - transform.position).normalized * Time.deltaTime * speed;
            base.Update();
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if (!inSequence)
            speed = (cat.transform.position - transform.position).magnitude/lengthOfOperation;
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
