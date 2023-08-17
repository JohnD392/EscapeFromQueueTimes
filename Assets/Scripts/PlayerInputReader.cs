using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public Vector3 moveVec = Vector3.zero;

    void OnMove(InputValue input) {
        moveVec = input.Get<Vector2>();
    }
}
