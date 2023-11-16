using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaceholderUi : MonoBehaviour
{
    // Reference to the button
    public Button button;
    public int buttonType;
    public string sceneName;
   

    void Start()
    {
        // Attach the method to be called when the button is pressed
        button.onClick.AddListener(TaskOnClick);
    }

    // This method will be called when the button is pressed
    void TaskOnClick()
    {
        Debug.Log("Button pressed!");
        if (buttonType == 0) 
        {
            SceneManager.LoadScene(sceneName);
        } 
        else if (buttonType == 1) 
        {
            Application.Quit();
        }
    }
}