using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneSequence : SequenceObject
{
    [SerializeField] FPSController player;

    public override void End()
    {
        base.End();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
