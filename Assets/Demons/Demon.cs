using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Demon : MonoBehaviour
{   
    public void Colour(Color colour)
    {
        Gameplay.ChildrenMaterialColour(gameObject, 0, colour);
    }
}
