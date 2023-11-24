using UnityEngine;

public class FireBallSequence : SequenceObject
{
    [SerializeField] Demon cat;
    [SerializeField] float speed;
    // Update is called once per frame
    protected override void Update()
    {
        transform.LookAt(cat.transform.position);
        if(inSequence)
        {
            transform.position += transform.forward * Gameplay.deltaTime * speed;
            lengthOfOperation += Gameplay.deltaTime;
            base.Update();
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
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
        if (explodee.gameObject.name.ToLower().Contains("cat"))
            End();
    }
}
