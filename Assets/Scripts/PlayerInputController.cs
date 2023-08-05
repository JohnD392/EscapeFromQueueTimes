using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterStateMachine))]
public class PlayerInputController : MonoBehaviour {
    public InputActionReference leftInputActionReference;
    public InputActionReference rightInputActionReference;
    public InputActionReference forwardInputActionReference;
    public InputActionReference backInputActionReference;
    
    public Vector3 GetInputVector() {
        Vector3 inputVec = Vector3.zero;
        if(leftInputActionReference.action.IsPressed()) inputVec -= transform.right;
        if(rightInputActionReference.action.IsPressed()) inputVec += transform.right;
        if(forwardInputActionReference.action.IsPressed()) inputVec += transform.forward;
        if(backInputActionReference.action.IsPressed()) inputVec -= transform.forward;

        return inputVec;
    }
}
