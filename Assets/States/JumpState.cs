using UnityEngine;

public class JumpState : ICharacterState {
    public void OnEnterState(GameObject character) {
        Debug.Log("JumpState OnEnterState");
        character.GetComponent<Rigidbody>().AddForce(Vector3.up * 200f, ForceMode.Impulse);
    }
    public void OnExitState(GameObject character) {
        Debug.Log("JumpState OnExitState");
    }
    public void Tick(GameObject character) {
        if (Vector3.Dot(character.GetComponent<Rigidbody>().velocity, Vector3.down) > 0f) {
            character.GetComponent<CharacterStateMachine>().ChangeState(CharacterStateMachine.idleState);
        }
    }
}