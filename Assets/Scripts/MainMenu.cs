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

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

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
        buttonBack2.onClick.AddListener(Back);

        sliderMasterVolume.onValueChanged.AddListener(MasterVolume);
        sliderMusicVolume.onValueChanged.AddListener(MusicVolume);
        sliderSFXVolume.onValueChanged.AddListener(SFXVolume);
        sliderDialogueVolume.onValueChanged.AddListener(DialogueVolume);

        hasStartedGame = false;

        pixels.heightPixelation = 500;
        pixels.widthPixelation = 500;
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

            GetComponent<Canvas>().worldCamera = pauseCamera;

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
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
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

    void MasterVolume(float f)
    {
        Gameplay.masterVolume = f;
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
