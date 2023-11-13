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

    InputActionMap controls; 

    public void Start()
    {
        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void MoveToTarget(InputActionMap controlsForFocus)
    {
        SetCameraTarget();
        
        controls = controlsForFocus;

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
        controls.Disable();

        targetPosition = playerCameraTransform.position;
        targetRotation = playerCameraTransform.rotation;

        fromPosition = initialPosition;
        fromRotation = initialRotation;

        transform.position = initialPosition;
        transform.rotation = initialRotation;


        elapsed = 0;
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            float interpolationRatio = elapsed / duration;

            Vector3 interpolatedPosition = Vector3.Lerp(fromPosition, targetPosition, interpolationRatio);
            Quaternion interpolatedRotation = Quaternion.Lerp(fromRotation, targetRotation, interpolationRatio);

            transform.position = interpolatedPosition;
            transform.rotation = interpolatedRotation;


            if (elapsed < duration)
                elapsed += Time.deltaTime;

            else
            {
                elapsed = 0;
                moving = false;

                //If the camera has finished moving, enable controlls of the new focus
                if (targetPosition == initialPosition)
                    controls.Enable();
                else
                {
                    playerController.controls.Player.Enable();
                    playerController.playerCamera.enabled = true;
                    GetComponent<Camera>().enabled = false;
                }
            }
        }
    }

    public void SetCameraTarget()
    {
        playerController.playerCamera.enabled = false;
        GetComponent<Camera>().enabled = true;

        playerController.controls.Player.Disable();
        //Breaks zoom so that zoom doesnt break
        playerController.playerCamera.fieldOfView = playerController.CameraDefaultFOV;
    }
}