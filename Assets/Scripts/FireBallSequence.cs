using UnityEngine;

public class FireBallSequence : SequenceObject
{
    [SerializeField] Demon cat;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(inSequence)
        {
            timeInOperation += Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            inSequence = timeInOperation < lengthOfOperation;
            if (!inSequence)
                End();
        }
    }

    public override void Begin(bool decision)
    {
        this.decision = decision;

        if (!inSequence)
        {
            inSequence = true;
            gameObject.SetActive(true);
            timeInOperation = 0;
            velocity = (cat.transform.position - transform.position)/lengthOfOperation;
        }
    }

    public override void End()
    {
        if(nextInSequence)
            nextInSequence.Begin(decision);

       gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        ExplosionChange explodee = other.gameObject.GetComponent<ExplosionChange>();
        if (explodee != null)
            explodee.Explode();
    }
}
