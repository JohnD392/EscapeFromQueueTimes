using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : MonoBehaviour {
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

    public void Update() {
        currentState.Tick(gameObject);
    }

    public void Start() {
        Initialize(shmovementState);
    }

    public void Jump() {
        float isJumping = jumpActionReference.action.ReadValue<float>();
        if (isJumping > .9f) ChangeState(jumpState);
    }
}
