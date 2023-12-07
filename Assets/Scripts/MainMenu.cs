using PSX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// even though this is called MainMenu, it controls the entire computer
// menu interface, including main menu, in-game menu, settings, credits

// nothing in the video settings is set up yet

public class MainMenu : MonoBehaviour
{
    public Computer computer;
    public PixelationController pixels;

    public Camera mainMenuCamera;
    public Camera pauseCamera;
    public GameObject titleGraphic;
    public TutorialSequence firstTutorialSequence;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject tutorialSkipMenu;
    public GameObject pauseMenu;

    // main menu
    public Button buttonPlay;
    public Button buttonSettings;
    public Button buttonQuit;
    public Button buttonCredits;

    // settings menu
    public Button buttonBack;
    public Button buttonAudioSettings;
    public Button buttonVideoSettings;
    public GameObject audioSettings;
    public GameObject videoSettings;

    // credits menu
    public Button buttonBack2;

    // settings control
    public Slider sliderMasterVolume;
    public TMP_Text textMasterVolume;
    public Slider sliderMusicVolume;
    public TMP_Text textMusicVolume;
    public Slider sliderSFXVolume;
    public TMP_Text textSFXVolume;
    public Slider sliderDialogueVolume;
    public TMP_Text textDialogueVolume;


    //TutorialSkip
    public Button buttonSkipYes;
    public Button buttonSkipNo;

    //Pause Menu
    [SerializeField] Button pauseResume;
    [SerializeField] Button pauseSettings;
    [SerializeField] Button pauseTutorial;
    [SerializeField] Button pauseMaineMenu;
    [SerializeField] Button pauseQuit;


    [HideInInspector] public bool hasStartedGame;

    // Start is called before the first frame update
    void Start()
    {
        Gameplay.active = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        buttonPlay.onClick.AddListener(TutorialSkip);
        buttonSettings.onClick.AddListener(Settings);
        buttonQuit.onClick.AddListener(Quit);
        buttonCredits.onClick.AddListener(Credits);

        buttonBack.onClick.AddListener(Back);

        buttonAudioSettings.onClick.AddListener(AudioSettings);
        buttonVideoSettings.onClick.AddListener(VideoSettings);
        buttonBack2.onClick.AddListener(Back);

        buttonSkipYes.onClick.AddListener(StartTutorial);
        buttonSkipNo.onClick.AddListener(Play);

        sliderMasterVolume.onValueChanged.AddListener(MasterVolume);
        sliderMusicVolume.onValueChanged.AddListener(MusicVolume);
        sliderSFXVolume.onValueChanged.AddListener(SFXVolume);
        sliderDialogueVolume.onValueChanged.AddListener(DialogueVolume);

        pauseResume.onClick.AddListener(Resume);
        pauseSettings.onClick.AddListener(Settings);
        pauseTutorial.onClick.AddListener(StartTutorial);
        pauseMaineMenu.onClick.AddListener(Restart);
        pauseQuit.onClick.AddListener(Quit);

        hasStartedGame = false;

        pixels.heightPixelation = 666;
        pixels.widthPixelation = 666;
    }


    void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        titleGraphic.SetActive(false);
        mainMenuCamera.GetComponent<CameraTransition>().MoveToPlayer();

        PauseScreenReset();

        GetComponent<Canvas>().worldCamera = pauseCamera;

        hasStartedGame = true;
        gameObject.SetActive(false);
    }

    void StartTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        titleGraphic.SetActive(false);

        GetComponent<Canvas>().worldCamera = pauseCamera;

        gameObject.SetActive(false);

        PauseScreenReset();
        mainMenuCamera.enabled = false;
        hasStartedGame = true;
        firstTutorialSequence.Begin(true);
    }


    void TutorialSkip()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        tutorialSkipMenu.SetActive(true);
    }
    void PauseScreenReset()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
        tutorialSkipMenu.SetActive(false);
    }

    void Settings()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void Back()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(!hasStartedGame);
        pauseMenu.SetActive(hasStartedGame);
    }

    void Quit()
    {
        Application.Quit();
    }

    void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        // create a useless CallbackContext just to get the function to run
        computer.Exit(new InputAction.CallbackContext());
        Gameplay.active = true;
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

    void MasterVolume(float f)
    {
        Gameplay.masterVolume = f * 100;
        textMasterVolume.text = (f * 100).ToString("0");
    }

    void MusicVolume(float f)
    {
        Gameplay.musicVolume = f;
        textMusicVolume.text = (f * 100).ToString("0");
    }

    void SFXVolume(float f)
    {
        Gameplay.sfxVolume = f;
        textSFXVolume.text = (f * 100).ToString("0");
    }

    void DialogueVolume(float f)
    {
        Gameplay.dialogueVolume = f;
        textDialogueVolume.text = (f * 100).ToString("0");
    }

}
