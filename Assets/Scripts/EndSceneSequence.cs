using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneSequence : SequenceObject
{
    // Update is called once per frame
    void Update()
    {
        if(inSequence)
            timeInOperation += Time.deltaTime;
        if (timeInOperation > lengthOfOperation)
            End();
    }

    public override void Begin(bool decision)
    {
        this.decision = decision;

        if (!inSequence)
        {
            inSequence = true;
            gameObject.SetActive(true);
            timeInOperation = 0;
        }
    }


    public override void End()
    {
        inSequence = false;
        timeInOperation = 0;
        SceneManager.LoadScene("WorldspaceCanvasTest");
    }
}
