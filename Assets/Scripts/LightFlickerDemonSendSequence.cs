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
        base.Update();
        if (numberOfFlickers == 1) Destroy(objectToDestroy);
    }

    public override void End()
    {
        aS.PlayOneShot(SuccessfulSummonNoise);
        dSS.ExpectedObject = null;
        dSS.summoning = false;
        base.End();
    }
}