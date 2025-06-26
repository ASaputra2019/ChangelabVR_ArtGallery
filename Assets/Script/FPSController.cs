using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [SerializeField] private GameObject interactorUI;
    [SerializeField] CharacterController characterController;
    //private NPCController nPCController;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public event Action playNext;
    private bool canInteract = false;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;
    float rotationY = 0f;

    [HideInInspector]
    public bool canMove = true;

    public void Start()
    {
       // this.nPCController = nPCController;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       // nPCController.CanInteract += CanInteract;
        rotationY = transform.rotation.eulerAngles.y;

        rotationX = playerCamera.transform.localEulerAngles.x;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotationX += -mouseY * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            rotationY += mouseX * lookSpeed;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
        Interact();
    }

    public void CanInteract()
    {
        canInteract = true;
    }

    private void Interact()
    {
        // if (canInteract)
        // {
        //     bool isInRange = Utils.IsInRange(playerCamera.transform, nPCController.transform);
        //     interactorUI.SetActive(isInRange);
        //     if (isInRange)
        //     {
        //         if (Input.GetKeyDown("e"))
        //         {
        //             playNext?.Invoke();
        //             canInteract = false;
        //             interactorUI.SetActive(false);
        //         }
        //     }

        // }
    }

    void OnDestroy()
    {
      //  nPCController.CanInteract -= CanInteract;
    }
}
