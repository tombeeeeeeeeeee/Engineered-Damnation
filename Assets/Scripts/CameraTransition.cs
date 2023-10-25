using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraTransition : MonoBehaviour
{
    public Transform player;

    Vector3 targetPosition;
    Quaternion targetRotation;

    float elapsed;
    public float duration;
    bool moving = false;

    public void StartMoving()
    {
        // camera should already be in the target position in editor
        targetPosition = transform.position;
        targetRotation = transform.rotation;

        transform.position = player.position;
        transform.rotation = player.rotation;

        elapsed = 0;
        moving = true;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            float interpolationRatio = elapsed / duration;

            Vector3 interpolatedPosition = Vector3.Lerp(player.position, targetPosition, interpolationRatio);
            Quaternion interpolatedRotation = Quaternion.Lerp(player.rotation, targetRotation, interpolationRatio);

            transform.position = interpolatedPosition;
            transform.rotation = interpolatedRotation;


            if (elapsed < duration)
            {
                elapsed++;
            }
            else
            {
                elapsed = 0;
                moving = false;
            }
        }
    }
}
