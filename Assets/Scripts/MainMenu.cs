using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Camera mainMenuCamera;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Button button;
    public Button button2;
    public Button buttonPlay;
    public GameObject titleGraphic;

    public bool isMainMenu;
    [HideInInspector] public bool hasStartedGame;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        button2.onClick.AddListener(TaskOnClick2);
        buttonPlay.onClick.AddListener(Play);
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

    void TaskOnClick()
    {
        Debug.Log("aaaa");
        updatemenu();
    }

    void TaskOnClick2()
    {
        updatemenu();
        Debug.Log("eeee");
    }

    void updatemenu() 
    {
        if (isMainMenu == true)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
            isMainMenu = false;
        }
        else if (isMainMenu == false)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            isMainMenu = true;
        }
    }
}
