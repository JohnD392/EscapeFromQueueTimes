using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : NetworkBehaviour {
	public ICharacterState currentState;

	[SerializeField]
	public InputActionReference jumpActionReference;

	public static ICharacterState jumpState = new JumpState();
	public static ICharacterState shmovementState = new MovementState();

	public Transform pivotPoint;

	public void Start() {
		Initialize(shmovementState);
	}

	public void FixedUpdate() {
		if(isLocalPlayer) currentState.Tick(gameObject);
	}

	public void Initialize(ICharacterState startingState) {
		currentState = startingState;
		startingState.OnEnterState(gameObject);
	}

	public void ChangeState(ICharacterState newState) {
		currentState.OnExitState(gameObject);
		currentState = newState;
		newState.OnEnterState(gameObject);
	}


	public bool InState(System.Type type) {
		if (currentState.GetType() == type) return true;
		return false;
	}

	void OnJump() {
		ChangeState(jumpState);
	}

	void OnLeanRight() {
		if(currentState.GetType() == typeof(LeanState)) ChangeState(shmovementState); // reset to normal when already leaning
		else ChangeState(new LeanState(this.pivotPoint, true)); // Lean right
	}

	void OnLeanLeft() {
		if (currentState.GetType() == typeof(LeanState)) ChangeState(shmovementState); // reset to normal when already leaning
		else ChangeState(new LeanState(this.pivotPoint, false)); // Lean left
	}
}
