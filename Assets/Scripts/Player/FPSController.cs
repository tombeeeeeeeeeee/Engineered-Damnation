using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [Header("Setup")]
    public Controls controls;
    public CharacterController cc;
    [Header("Movement")]
    public float walkingSpeed = 7.5f;
    [Header("Camera")]
    public float lookSpeed = 2.0f;
    public float CameraDefaultFOV = 60;
    public float CameraZoomFOV = 15;
    public Camera bookCamera;
    public Camera playerCamera;

    //Input System
    private Vector2 lookInput;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        playerCamera.fieldOfView = CameraDefaultFOV;
        if (controls == null)
            controls = new Controls();

        controls.Player.Zoom.performed += CameraZoom;


        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controls.Player.Enable();
    }

    void Update() 
    {
        // PLAYER MOVEMENT
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>() * walkingSpeed * Time.deltaTime;
        moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        cc.Move(moveDirection);

        // CAMERA MOVEMENT
        // Read input for mouse movement and rotate and camera
        lookInput = controls.Player.Camera.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * lookInput.x);

        float cameraRotationX = -lookInput.y;
        float currentRotationX = playerCamera.transform.localEulerAngles.x;

        currentRotationX += cameraRotationX;

        // Keep camera rotation within certain bounds
        if (currentRotationX > 180f)
            currentRotationX -= 360f;

        currentRotationX = Mathf.Clamp(currentRotationX, -89f, 89f);

        playerCamera.transform.localEulerAngles = new Vector3(currentRotationX, 0f, 0f);
    }


    public void CameraZoom(InputAction.CallbackContext context)
    {
        playerCamera.fieldOfView = playerCamera.fieldOfView == CameraDefaultFOV ? CameraZoomFOV : CameraDefaultFOV;
    }
}