using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float lookSensitivity = 0.2f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody rigidBody;
    BoxCollider boxCollider;
    Camera playerCamera;

    Vector2 moveInputVector;
    Vector2 lookInputVector;

    public Vector3 ClampViewAngle(Transform playerCamera)
    {
        if (playerCamera.up.y < 0f)
        {
            if (playerCamera.forward.y > 0f)
            {
                return new Vector3(270f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
            }
            return new Vector3(90f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
        }

        return playerCamera.localEulerAngles;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        Vector3 movementDelta = new Vector3(moveInputVector.x * moveSpeed * Time.deltaTime, 0, moveInputVector.y * moveSpeed * Time.deltaTime);
        transform.Translate(movementDelta);
    }

    void Look()
    {
        Vector2 delta = lookInputVector * lookSensitivity;
        playerCamera.transform.Rotate(-delta.y, 0f, 0f);
        playerCamera.transform.localEulerAngles = ClampViewAngle(playerCamera.transform);
        transform.Rotate(new Vector3(0f, delta.x, 0f));
    }

    void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
    }

    void OnMove(InputValue value)
    {
        moveInputVector = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInputVector = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Jump();
        }
    }
}
