using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float CameraDefaultFOV = 60;
    public float CameraZoomFOV = 15;
    public bool rotationLocked = false;


    Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;
    public Controls controls;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerCamera.fieldOfView = CameraDefaultFOV;

        controls = new Controls();
        controls.Enable();
        controls.Player.Zoom.performed += CameraZoom;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        moveDirection = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = (transform.forward * moveDirection.y + transform.right * moveDirection.x)  * walkingSpeed * Time.deltaTime;

        Vector2 mouseDelta = controls.Player.Camera.ReadValue<Vector2>();
        if (rotationLocked)
            mouseDelta = Vector2.zero;
        transform.Rotate(new Vector3(0,mouseDelta.x,0) * lookSpeed * Time.deltaTime);
        playerCamera.transform.Rotate(new Vector3(-mouseDelta.y,0,0) * lookSpeed * Time.deltaTime);
    }


    public void CameraZoom(InputAction.CallbackContext context)
    {
        playerCamera.fieldOfView = playerCamera.fieldOfView == CameraDefaultFOV ? CameraZoomFOV : CameraDefaultFOV;
    }
}