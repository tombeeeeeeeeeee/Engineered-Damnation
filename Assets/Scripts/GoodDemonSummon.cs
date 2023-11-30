using UnityEngine;
using UnityEngine.Rendering;

public class GoodDemonSummon : SequenceObject
{
    [SerializeField] DemonSummoningSpot dSS;
    private GameObject demon;
    [SerializeField] Material shader;

    public override void Begin(bool decision)
    {
        base.Begin(decision);
        if(inSequence)
        {
            shader.SetColor("_shadercolor", dSS.shaderColourToSummon);
            demon = dSS.demonToSummon;
            demon.GetComponent<Demon>().Colour(dSS.colourToSummon);
            demon.GetComponent<Demon>().Summon(transform.position);
        }
    }

    public override void End()
    {
        if (nextInSequence.gameObject.GetComponent<LightFlickerDemonSendSequence>() != null)
            nextInSequence.gameObject.GetComponent<LightFlickerDemonSendSequence>().objectToDestroy = demon;
        base.End();
    }
}
