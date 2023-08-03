using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : MonoBehaviour {
    ICharacterState currentState;

    [SerializeField]
    public InputActionReference jump;

    static ICharacterState idleState = new IdleState();
    static ICharacterState movementState = new MovementState();
    static ICharacterState jumpState = new JumpState();

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
        Initialize(jumpState);
    }
}

public interface ICharacterState {
    public void Tick(GameObject character);
    public void OnEnterState(GameObject character);
    public void OnExitState(GameObject character);
}

public class IdleState : ICharacterState {

    public void OnEnterState(GameObject character) {
        Debug.Log("I'm gonna chill now");
    }

    public void OnExitState(GameObject character) {
        Debug.Log("I'm done chillin");
    }

    public void Tick(GameObject character) {
        CharacterStateMachine csm = character.GetComponent<CharacterStateMachine>();
        float isJumping = csm.jump.action.ReadValue<float>();
        Debug.Log("IsJumping: " + isJumping);
        if(isJumping > .9f) csm.ChangeState(new JumpState());
    }
}

public class MovementState : ICharacterState {
    public void OnEnterState(GameObject character) {
        character.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }

    public void OnExitState(GameObject character) {

    }

    public void Tick(GameObject character) {

    }
}

public class JumpState : ICharacterState {
    public void OnEnterState(GameObject character) {
        Debug.Log("JumpState OnEnterState");
        character.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000f, ForceMode.Impulse);
        character.GetComponent<CharacterStateMachine>().ChangeState(new IdleState());
    }

    public void OnExitState(GameObject character) {
        Debug.Log("JumpState OnExitState");
    }

    public void Tick(GameObject character) {
        Debug.Log("I'm Jumping!");
    }
}