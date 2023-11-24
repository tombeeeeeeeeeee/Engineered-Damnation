using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascension : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.up * Gameplay.deltaTime * 0.2f;
    }
}
