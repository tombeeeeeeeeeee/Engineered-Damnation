using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float CameraDefaultFOV = 60;
    public float CameraZoomFOV = 15;
    public bool rotationLocked = false;


    Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;
    public Controls controls;
    private CharacterController cc;

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
        controls.Enable();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>() * walkingSpeed * Time.deltaTime;
        moveDirection = transform.forward * moveInput.y +  transform.right * moveInput.x;
        cc.Move(moveDirection);

        Vector2 mouseDelta = rotationLocked ? Vector2.zero : controls.Player.Camera.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0,mouseDelta.x,0));

        playerCamera.transform.Rotate(new Vector3(-mouseDelta.y,0,0));
        if (playerCamera.transform.localRotation.x > 0.6) playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(285, 0,0));
        else if (playerCamera.transform.localRotation.x < -0.6)  playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(-285, 0,0));
    }


    public void CameraZoom(InputAction.CallbackContext context)
    {
        playerCamera.fieldOfView = playerCamera.fieldOfView == CameraDefaultFOV ? CameraZoomFOV : CameraDefaultFOV;
    }
}