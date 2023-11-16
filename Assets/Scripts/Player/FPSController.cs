using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] Canvas PauseScreen;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        playerCamera.fieldOfView = CameraDefaultFOV;
        if (controls == null)
            controls = new Controls();
        controls.Player.Zoom.performed += CameraZoom;
        controls.Player.Pause.performed += Pause;
        controls.PauseMenu.Unpause.performed += Unpause;

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
        Vector2 mouseDelta = rotationLocked ? Vector2.zero : controls.Player.Camera.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0, mouseDelta.x, 0));

        playerCamera.transform.Rotate(new Vector3(-mouseDelta.y, 0, 0));
        if (playerCamera.transform.localRotation.x > 0.6) playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(285, 0, 0));
        else if (playerCamera.transform.localRotation.x < -0.6) playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(-285, 0, 0));


    }


    public void CameraZoom(InputAction.CallbackContext context)
    {
        playerCamera.fieldOfView = context.ReadValueAsButton() ? CameraZoomFOV : CameraDefaultFOV;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        controls.Player.Disable();
        controls.PauseMenu.Enable();
        PauseUnpause(true);
    }

    private void Unpause(InputAction.CallbackContext context)
    {
        controls.Player.Enable();
        controls.PauseMenu.Disable();
        PauseUnpause(false);
    }
    private void PauseUnpause(bool pause)
    {
        PauseScreen.gameObject.SetActive(pause);
        PauseScreen.enabled = pause;
        Time.timeScale = pause ? 0 : 1;
    }

}