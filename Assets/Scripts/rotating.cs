using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,Gameplay.deltaTime * 200,0));
    }
}
