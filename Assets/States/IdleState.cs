using UnityEngine;

public class IdleState : ICharacterState {

    public void OnEnterState(GameObject character) {
        Debug.Log("I'm gonna chill now");
    }

    public void OnExitState(GameObject character) {
        Debug.Log("I'm done chillin");
    }

    public void Tick(GameObject character) {
        Jump(character);
    }

    public void Jump(GameObject character) {
        CharacterStateMachine csm = character.GetComponent<CharacterStateMachine>();
        float isJumping = csm.jump.action.ReadValue<float>();
        Debug.Log("IsJumping: " + isJumping);
        if (isJumping > .9f) csm.ChangeState(new JumpState());
    }
}