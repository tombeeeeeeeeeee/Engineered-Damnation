using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraTransition : MonoBehaviour
{
    public FPSController playerController;
    public Transform playerCameraTransform;
    public float duration;

    Vector3 initialPosition;
    Quaternion initialRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;

    Vector3 fromPosition;
    Quaternion fromRotation;

    float elapsed;
    bool moving = false;
    Camera thisCamera;

    public void Start()
    {
        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        thisCamera = GetComponent<Camera>();
    }

    public void MoveToTarget()
    {
        SetCameraBook();

        targetPosition = initialPosition;
        targetRotation = initialRotation;

        fromPosition = playerCameraTransform.position;
        fromRotation = playerCameraTransform.rotation;

        transform.position = playerCameraTransform.position;
        transform.rotation = playerCameraTransform.rotation;

        elapsed = 0;
        moving = true;
    }

    public void MoveToPlayer()
    {
        // cursed way of accounting for the difference between FixedUpdate fps (50) and actual fps (60)
        Invoke("SetCameraPlayer", (float)((duration + (duration * 0.177)) / 60));

        targetPosition = playerCameraTransform.position;
        targetRotation = playerCameraTransform.rotation;

        fromPosition = initialPosition;
        fromRotation = initialRotation;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        elapsed = 0;
        moving = true;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            float interpolationRatio = elapsed / duration;

            Vector3 interpolatedPosition = Vector3.Lerp(fromPosition, targetPosition, interpolationRatio);
            Quaternion interpolatedRotation = Quaternion.Lerp(fromRotation, targetRotation, interpolationRatio);

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

    public void SetCameraPlayer()
    {
        playerController.playerCamera.enabled = true;
        thisCamera.enabled = false;

        playerController.controls.Player.Enable();
        playerController.controls.Focused.Disable();
    }

    public void SetCameraBook()
    {
        playerController.playerCamera.enabled = false;
        thisCamera.enabled = true;

        playerController.controls.Player.Disable();
        playerController.controls.Focused.Enable();
    }
}