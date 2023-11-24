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
    public float depixilationPercentage;
    [SerializeField] PixelationController pixels;
    private float startingPixelHeight;
    private float startingPixelWidth;

    Vector3 initialPosition;
    Quaternion initialRotation;
    float initialFOV;

    Vector3 targetPosition;
    Quaternion targetRotation;
    float targetFOV;

    Vector3 fromPosition;
    Quaternion fromRotation;
    float fromFOV;


    float elapsed;
    bool moving = false;

    InputActionMap controls; 

    public void Start()
    {
        // camera should already be in the target position in editor
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialFOV = GetComponent<Camera>().fieldOfView;
        if(pixels)
        {
            startingPixelHeight = pixels.heightPixelation;
            startingPixelWidth = pixels.widthPixelation;
        }  
    }

    public void MoveToTarget(InputActionMap controlsForFocus)
    {
        SetCameraTarget();
        
        controls = controlsForFocus;

        targetPosition = initialPosition;
        targetRotation = initialRotation;
        targetFOV = initialFOV;

        fromPosition = playerCameraTransform.position;
        fromRotation = playerCameraTransform.rotation;
        fromFOV = playerController.playerCamera.fieldOfView;

        depixilationPercentage = -depixilationPercentage;

        elapsed = 0;
        moving = true;
    }

    public void MoveToPlayer()
    {
        controls.Disable();

        targetPosition = playerCameraTransform.position;
        targetRotation = playerCameraTransform.rotation;
        targetFOV = playerController.playerCamera.fieldOfView;

        fromPosition = initialPosition;
        fromRotation = initialRotation;
        fromFOV = initialFOV;

        depixilationPercentage = -depixilationPercentage;

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

            if(pixels) pixels.heightPixelation += interpolationRatio * depixilationPercentage;
            if(pixels) pixels.widthPixelation += interpolationRatio * depixilationPercentage;
            

            GetComponent<Camera>().fieldOfView = fromFOV - interpolationRatio * (fromFOV - targetFOV);
            
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
                    if(pixels) pixels.heightPixelation = startingPixelHeight;
                    if(pixels) pixels.widthPixelation = startingPixelWidth;
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