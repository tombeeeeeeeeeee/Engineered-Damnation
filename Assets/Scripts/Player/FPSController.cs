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
    public Camera playerCamera;
    [SerializeField] CameraTransition pauseCamera;
    [SerializeField] Computer pauseComputer;
    public float pixelWidth;
    public float pixelHeight;


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
        controls.Player.Pause.performed += Pause;
    }

    void Update() 
    {
        // PLAYER MOVEMENT
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>() * walkingSpeed * Gameplay.deltaTime;
        moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        cc.Move(moveDirection);

        // CAMERA MOVEMENT
        // Read input for mouse movement and rotate and camera
        lookInput = controls.Player.Camera.ReadValue<Vector2>() * lookSpeed * Gameplay.deltaTime;
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

    private void Pause(InputAction.CallbackContext context)
    {
        PauseToggle(true);
    }
    
    private void PauseToggle(bool pause)
    {
        pauseCamera.MoveToTarget(controls.Focused);
        pauseComputer.Init();
        Gameplay.gameplayActive = !pause;
    }

    public void CameraZoom(InputAction.CallbackContext context)
    {
        playerCamera.fieldOfView = context.ReadValueAsButton() ? CameraZoomFOV : CameraDefaultFOV;
    }

    public void PlayerEnable()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controls.Player.Enable();
        playerCamera.enabled = true;
    }
}