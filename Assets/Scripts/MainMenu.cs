using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Computer computer;

    public Camera mainMenuCamera;
    public GameObject titleGraphic;

    public GameObject mainMenu;
    public GameObject settingsMenu;

    // main menu
    public Button buttonPlay;
    public Button buttonResume;
    public Button buttonSettings;
    public Button buttonQuit;
    public Button buttonCredits;

    // settings menu
    public Button buttonBack;
    public Button buttonAudioSettings;
    public Button buttonVideoSettings;
    public GameObject audioSettings;
    public GameObject videoSettings;

    [HideInInspector] public bool hasStartedGame;

    // Start is called before the first frame update
    void Start()
    {
        buttonPlay.onClick.AddListener(Play);
        buttonSettings.onClick.AddListener(Settings);
        buttonQuit.onClick.AddListener(Quit);
        buttonCredits.onClick.AddListener(Credits);
        buttonResume.onClick.AddListener(Resume);
        buttonBack.onClick.AddListener(Back);
        buttonAudioSettings.onClick.AddListener(AudioSettings);
        buttonVideoSettings.onClick.AddListener(VideoSettings);

        hasStartedGame = false;
    }

    void Play()
    {
        if (!hasStartedGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            titleGraphic.SetActive(false);
            mainMenuCamera.GetComponent<CameraTransition>().MoveToPlayer();

            buttonPlay.gameObject.SetActive(false);
            buttonResume.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void Back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void Quit()
    {
        Application.Quit();
    }

    void Credits()
    {

    }

    void Resume()
    {
        // create a useless CallbackContext just to get the function to run
        computer.Exit(new InputAction.CallbackContext());
    }

    void AudioSettings()
    {
        videoSettings.SetActive(false);
        audioSettings.SetActive(true);
    }

    void VideoSettings()
    {
        audioSettings.SetActive(false);
        videoSettings.SetActive(true);
    }
}
