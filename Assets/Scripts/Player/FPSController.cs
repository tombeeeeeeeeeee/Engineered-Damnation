using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public Controls controls;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        controls = new Controls();
        //controls.Player.Move.performed;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 movement = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = Vector3.ClampMagnitude(new Vector3(movement.x, 0, movement.y), 1) * walkingSpeed * Time.deltaTime;
    }
}