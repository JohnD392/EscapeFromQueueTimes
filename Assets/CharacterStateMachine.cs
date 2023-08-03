using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : MonoBehaviour {
    ICharacterState currentState;

    [SerializeField]
    public InputActionReference jump;

    public static ICharacterState idleState = new IdleState();
    public static ICharacterState jumpState = new JumpState();
    public static ICharacterState shmovementState = new MovementState();

    public void Initialize(ICharacterState startingState) {
        currentState = startingState;
        startingState.OnEnterState(this.gameObject);
    }

    public void ChangeState(ICharacterState newState) {
        currentState.OnExitState(this.gameObject);
        currentState = newState;
        newState.OnEnterState(this.gameObject);
    }

    public void Update() {
        currentState.Tick(this.gameObject);
    }

    public void Start() {
        Initialize(shmovementState);
    }
}
