using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraTransition : MonoBehaviour
{
    public FPSController playerController;

    Transform playerTransform;

    Vector3 initialPosition;
    Quaternion initialRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;

    float elapsed;
    public float duration;
    bool moving = false;

    public void Start()
    {
        playerTransform = playerController.gameObject.transform;

        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void MoveToTarget()
    {
        SetCameraBook();

        targetPosition = initialPosition;
        targetRotation = initialRotation;

        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;

        elapsed = 0;
        moving = true;
    }

    public void MoveFromTarget()
    {
        Invoke("SetCameraPlayer", duration / 100);

        targetPosition = playerTransform.position;
        targetRotation = playerTransform.rotation;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        elapsed = 0;
        moving = true;
    }

    public void SetCameraPlayer()
    {
        playerController.playerCamera.enabled = true;
        playerController.bookCamera.enabled = false;
    }

    public void SetCameraBook()
    {
        playerController.playerCamera.enabled = false;
        playerController.bookCamera.enabled = true;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            float interpolationRatio = elapsed / duration;

            Vector3 interpolatedPosition = Vector3.Lerp(playerTransform.position, targetPosition, interpolationRatio);
            Quaternion interpolatedRotation = Quaternion.Lerp(playerTransform.rotation, targetRotation, interpolationRatio);

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
