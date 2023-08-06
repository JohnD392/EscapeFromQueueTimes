using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour {
    public Transform playerCamera;
    public float sensitivity;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() {
        Vector2 delta = Mouse.current.delta.ReadValue() * sensitivity * Time.deltaTime;
        playerCamera.Rotate(new Vector3(-delta.y, 0f, 0f));
        playerCamera.localEulerAngles = ClampViewAngle(playerCamera);

        float rotationAmount = delta.x;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationAmount);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }

    public Vector3 ClampViewAngle(Transform playerCamera) {
        if (playerCamera.up.y < 0f) {
            if (playerCamera.forward.y > 0f) return new Vector3(270f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
            return new Vector3(90f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
        }
        return playerCamera.localEulerAngles;
    }
}
