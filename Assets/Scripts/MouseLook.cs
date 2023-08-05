using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerCamera;
    public Transform playerTransform;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 delta = Mouse.current.delta.ReadValue() * 0.2f;
        playerCamera.Rotate(new Vector3(-delta.y, 0f, 0f));
        playerCamera.localEulerAngles = ClampViewAngle(playerCamera);
        transform.Rotate(new Vector3(0f, delta.x, 0f));
    }
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
}
