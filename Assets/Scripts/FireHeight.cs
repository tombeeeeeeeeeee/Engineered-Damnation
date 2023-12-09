using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeight : MonoBehaviour
{
    [SerializeField] float fireHeight = 8;

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(Gameplay.completionRate > 0);
        gameObject.GetComponent<ParticleSystem>().startLifetime = fireHeight * Gameplay.completionRate;
    }
}
