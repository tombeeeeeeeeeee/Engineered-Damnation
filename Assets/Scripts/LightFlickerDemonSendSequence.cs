using UnityEngine;
public class LightFlickerDemonSendSequence:LightFlickerOffSequence
{
    public GameObject objectToDestroy;
    private AudioSource aS;
    [SerializeField] AudioClip SuccessfulSummonNoise;
    [SerializeField] DemonSummoningSpot dSS;

    private void Start()
    {
        lightToFlicker = GetComponent<Light>();
        aS = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (numberOfFlickers == 1 && objectToDestroy) Destroy(objectToDestroy);
        base.Update();

    }

    public override void End()
    {
        if (inSequence)
        {
            aS.PlayOneShot(SuccessfulSummonNoise);
            dSS.ExpectedObject = null;
            Debug.Log("Ending The Sequence");
            dSS.summoning = false;
        }
        base.End();
    }
}