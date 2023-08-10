using UnityEngine;

public class JumpState : ICharacterState {
    public float jumpSpeed = 3f;

    public void OnEnterState(GameObject character) {
        Debug.Log("JumpState OnEnterState");
        character.GetComponent<Rigidbody>().velocity = new Vector3(
            character.GetComponent<Rigidbody>().velocity.x,
            Mathf.Max(character.GetComponent<Rigidbody>().velocity.y, jumpSpeed),
            character.GetComponent<Rigidbody>().velocity.z
        );
    }
    public void OnExitState(GameObject character) {
        Debug.Log("JumpState OnExitState");
    }
    public void Tick(GameObject character) {
        // If falling, re-enable jump
        if (-character.GetComponent<Rigidbody>().velocity.y > 0f) {
            character.GetComponent<CharacterStateMachine>().ChangeState(CharacterStateMachine.shmovementState);
        }
    }
}