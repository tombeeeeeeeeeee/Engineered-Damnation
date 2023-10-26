using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void Start()
    {
        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
        float t = (float)((duration + (duration * 0.17)) / 60);
        Invoke("SetCameraPlayer", t);
        Debug.Log(t);

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
        playerController.bookCamera.enabled = false;

        playerController.controls.Player.Enable();
        playerController.controls.Focused.Disable();
        Debug.Log("back to player");
    }

    public void SetCameraBook()
    {
        playerController.playerCamera.enabled = false;
        playerController.bookCamera.enabled = true;

        playerController.controls.Player.Disable();
        playerController.controls.Focused.Enable();
        Debug.Log("go to book");

    }
}
