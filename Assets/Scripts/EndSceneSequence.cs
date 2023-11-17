using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneSequence : SequenceObject
{
    // Update is called once per frame
    protected override void Update()
    {
        if(inSequence)
            base.Update();
    }

    public override void End()
    {
        base.End();
        SceneManager.LoadScene(0);
    }
}
