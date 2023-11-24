using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Camera mainMenuCamera;
    public GameObject titleGraphic;

    public GameObject mainMenu;
    public GameObject settingsMenu;

    // main menu
    public Button buttonPlay;
    public Button buttonSettings;

    // settings menu
    public Button buttonBack;
    public Button buttonAudioSettings;
    public Button buttonVideoSettings;
    public GameObject audioSettings;
    public GameObject videoSettings;

    [HideInInspector] public bool hasStartedGame;
    bool trueForMainFalseForSettings = true;

    // Start is called before the first frame update
    void Start()
    {
        buttonPlay.onClick.AddListener(Play);
        buttonSettings.onClick.AddListener(Settings);
        buttonBack.onClick.AddListener(Back);
        buttonAudioSettings.onClick.AddListener(AudioSettings);
        buttonVideoSettings.onClick.AddListener(VideoSettings);

        hasStartedGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        if (!hasStartedGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            titleGraphic.SetActive(false);
            mainMenuCamera.GetComponent<CameraTransition>().MoveToPlayer();
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
