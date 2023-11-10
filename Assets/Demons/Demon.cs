using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] GameObject[] colourableComponents;
    
    public void Colour(Color colour)
    {
        foreach(GameObject colourableComponent in colourableComponents)
            colourableComponent.GetComponent<MeshRenderer>().material.color = colour;
    }
}
