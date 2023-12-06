using PSX;
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
    public float initialPixelHeight;
    public float initialPixelWidth;

    [SerializeReference] PixelationController pixels;

    Vector3 initialPosition;
    Quaternion initialRotation;
    float initialFOV;

    Vector3 targetPosition;
    Quaternion targetRotation;
    float targetFOV;
    float targetPixelHeight;
    float targetPixelWidth;

    Vector3 fromPosition;
    Quaternion fromRotation;
    float fromFOV;
    float fromPixelHeight;
    float fromPixelWidth;


    float elapsed;
    bool moving = false;

    InputActionMap controls; 

    public void Start()
    {
        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialFOV = GetComponent<Camera>().fieldOfView;
    }

    public void MoveToTarget(InputActionMap controlsForFocus)
    {
        SetCameraTarget();
        
        controls = controlsForFocus;

        targetPosition = initialPosition;
        targetRotation = initialRotation;
        targetFOV = initialFOV;
        targetPixelHeight = initialPixelHeight;
        targetPixelWidth = initialPixelWidth;

        fromPosition = playerCameraTransform.position;
        fromRotation = playerCameraTransform.rotation;
        fromFOV = playerController.playerCamera.fieldOfView;
        fromPixelHeight = playerController.pixelHeight;
        fromPixelWidth = playerController.pixelWidth;

        elapsed = 0;
        moving = true;
    }

    public void MoveToPlayer()
    {
        if (controls != null)
            controls.Disable();

        targetPosition = playerCameraTransform.position;
        targetRotation = playerCameraTransform.rotation;
        targetFOV = playerController.playerCamera.fieldOfView;
        targetPixelHeight = playerController.pixelHeight;
        targetPixelWidth = playerController.pixelWidth;

        fromPosition = initialPosition;
        fromRotation = initialRotation;
        fromFOV = initialFOV;
        fromPixelHeight = initialPixelHeight;
        fromPixelWidth = initialPixelWidth;

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

            if(pixels) pixels.heightPixelation = Mathf.Lerp(fromPixelHeight, targetPixelHeight, interpolationRatio);
            if(pixels) pixels.widthPixelation = Mathf.Lerp(fromPixelWidth, targetPixelWidth, interpolationRatio);


            GetComponent<Camera>().fieldOfView = fromFOV - interpolationRatio * (fromFOV - targetFOV);

            if (elapsed < duration && Gameplay.active)
                elapsed += Gameplay.deltaTime;
            else if (elapsed < duration && !Gameplay.active)
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
                    Gameplay.active = true;
                    playerController.controls.Player.Enable();
                    playerController.playerCamera.enabled = true;
                    GetComponent<Camera>().enabled = false;
                }
            }
        }
        else
        {

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