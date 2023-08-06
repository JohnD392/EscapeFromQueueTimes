using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class CharacterStateMachine : NetworkBehaviour {
    ICharacterState currentState;

    [SerializeField]
    public InputActionReference jumpActionReference;

    public static ICharacterState jumpState = new JumpState();
    public static ICharacterState shmovementState = new MovementState();

    public void Initialize(ICharacterState startingState) {
        currentState = startingState;
        startingState.OnEnterState(gameObject);
    }

    public void ChangeState(ICharacterState newState) {
        currentState.OnExitState(gameObject);
        currentState = newState;
        newState.OnEnterState(gameObject);
    }

    public void FixedUpdate() {
        if(isLocalPlayer) currentState.Tick(gameObject);
    }

    public void Start() {
        Initialize(shmovementState);
    }

    public void Jump() {
        float isJumping = jumpActionReference.action.ReadValue<float>();
        if (isJumping > .9f) ChangeState(jumpState);
    }
}
