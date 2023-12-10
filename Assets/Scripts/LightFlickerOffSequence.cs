using UnityEngine;

[RequireComponent(typeof(Light))]

public class LightFlickerOffSequence : SequenceObject
{
    public int numberOfFlickers;
    public float timeBetweenFlicker = 0.3f;
    private float timeSinceLastFlicker = 0;
    protected Light lightToFlicker;
    [SerializeField] AudioClip flickerNoise;
    private void Start()
    {
        lightToFlicker = GetComponent<Light>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence) 
        {
            //Forces sequence to end when we choose.
            lengthOfOperation += 1;
            timeSinceLastFlicker += Gameplay.deltaTime;

            //If finished flickering
            if(numberOfFlickers <= 0  || Gameplay.isFinished)  End(); 

            //flicker!
            else if(timeSinceLastFlicker > timeBetweenFlicker)
            {
                timeSinceLastFlicker = 0;
                lightToFlicker.enabled = !lightToFlicker.enabled;
                numberOfFlickers--;
                if (numberOfFlickers % 2 == 0 && flickerNoise != null) 
                    gameObject.GetComponent<AudioSource>().PlayOneShot(flickerNoise);
                lengthOfOperation = 0;
            }
        }
    }

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        numberOfFlickers = Random.Range(2, 6);
        numberOfFlickers *= 2;
    }
}
