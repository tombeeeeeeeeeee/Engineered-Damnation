using UnityEngine;
using UnityEngine.Rendering;

public class GoodDemonSummon : SequenceObject
{
    [SerializeField] DemonSummoningSpot dSS;
    private GameObject demon;

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            demon = dSS.demonToSummon;
            demon = Instantiate(demon, transform);
        }
    }

    public override void End()
    {
        if (nextInSequence.gameObject.GetComponent<LightFlickerDemonSendSequence>() != null)
            nextInSequence.gameObject.GetComponent<LightFlickerDemonSendSequence>().objectToDestroy = demon;
        base.End();
    }
}
