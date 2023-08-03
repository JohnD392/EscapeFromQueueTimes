using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : MonoBehaviour {
    ICharacterState currentState;

    [SerializeField]
    public InputActionReference jump;

    static ICharacterState idleState = new IdleState();
    static ICharacterState movementState = new MovementState();

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

    public void Tick(GameObject character) { }
}

public class MovementState : ICharacterState {
    public void OnEnterState(GameObject character) {

    }

    public void OnExitState(GameObject character) {

    }

    public void Tick(GameObject character) {

    }
}