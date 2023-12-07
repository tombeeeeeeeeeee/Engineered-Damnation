using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float strength;
    [SerializeField] float duration;

    float durationLeft;
    bool doCameraShake = false;

    void Update()
    {
        if (doCameraShake)
        {
            durationLeft -= Time.deltaTime;

            

            if (durationLeft <= 0)
            {
                duration = 0;
                doCameraShake = false;
            }
        }
    }

    public void SetCameraShakeValues(float strength, float duration)
    {
        this.strength = strength;
        this.duration = duration;
    }

    public void StartCamerShake()
    {
        durationLeft = duration;
        doCameraShake = true;
    }
}
