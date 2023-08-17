using UnityEngine;

public class JumpState : ICharacterState {
    public float jumpSpeed = 3f;

    public void OnEnterState(Character character) {
        Debug.Log("JumpState OnEnterState");
        character.GetComponent<Rigidbody>().velocity = new Vector3(
            character.GetComponent<Rigidbody>().velocity.x,
            Mathf.Max(character.GetComponent<Rigidbody>().velocity.y, jumpSpeed),
            character.GetComponent<Rigidbody>().velocity.z
        );
    }
    public void OnExitState(Character character) { }
    public void Tick(Character character) { }
}