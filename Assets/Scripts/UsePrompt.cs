using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UsePrompt : MonoBehaviour
{
    public GameObject promptText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            promptText.SetActive(true);
        }
           
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            promptText.SetActive(false);
        }
    }
}
