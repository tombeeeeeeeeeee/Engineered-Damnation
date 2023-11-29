using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialSequence : SequenceObject
{
    [SerializeField] Camera cutsceneCamera;
    [SerializeField] string subtitlesString;
    [SerializeField] TextMeshProUGUI subtitles;
    [SerializeField] Canvas subtitleBackground;
    [SerializeField] Image focusImage;
    [SerializeField] AudioSource intercom;
    [SerializeField] AudioClip vLine;
    [SerializeField] FPSController player;
    public void endSequenceEarly(InputAction.CallbackContext context)
    {
        intercom.Stop();
        End();
    }

    protected override void Update()
    {
        if (inSequence)
        {
            timeInOperation += Time.deltaTime;
            inSequence = timeInOperation < lengthOfOperation;
            if (!inSequence)
                End();
        }
    }


    public override void End()
    {
        subtitles.text = "";
        focusImage.gameObject.SetActive(false);
        player.controls.Tutorial.End.performed -= endSequenceEarly;
        subtitleBackground.gameObject.SetActive(false);

        inSequence = false;
        timeInOperation = 0;

        if (nextInSequence != null)
        {
            cutsceneCamera.enabled = false;
            nextInSequence.Begin(decision);
        }
        else
            StartGame();
    }

    public override void Begin(bool decision)
    {
        cutsceneCamera.enabled = true;
        subtitles.text = subtitlesString;
        subtitleBackground.gameObject.SetActive(true);
        focusImage.gameObject.SetActive(true);

        base.Begin(decision);

        intercom.clip = vLine;
        intercom.Play();
        lengthOfOperation = vLine.length;
        player.controls.Tutorial.Enable();
        player.controls.Tutorial.End.performed += endSequenceEarly;
    }

    private void StartGame()
    {
        subtitleBackground.gameObject.SetActive(false); 
        player.controls.Tutorial.End.Disable();
        cutsceneCamera.GetComponent<CameraTransition>().MoveToPlayer();
    }
}
