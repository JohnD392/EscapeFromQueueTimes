using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStateMachine : NetworkBehaviour {
	public ICharacterState currentState;

	public Transform basePivotTransform;
	public Transform pivotTransform;

	public Transform cameraTransform;

	public delegate void ADSEventHandler();
	public static event ADSEventHandler OnADS;

	public delegate void StopADSEventHandler();
	public static event StopADSEventHandler OnStopADS;

	public delegate void DeathEventHandler();
	public static event DeathEventHandler OnDeath;

	[SerializeField]
	public InputActionReference jumpActionReference;

	public static ICharacterState jumpState;
	public static ICharacterState shmovementState;
	public static ICharacterState crouchState;

	public void Start() {
		jumpState = new JumpState();
		shmovementState = new MovementState();
		crouchState = new CrouchState(basePivotTransform, pivotTransform);
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

	public void ADS() {
		OnADS();
    }

	public void StopADS() {
		OnStopADS();
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
		else ChangeState(new LeanState(this.pivotTransform, true)); // Lean right
	}

	void OnLeanLeft() {
		if (currentState.GetType() == typeof(LeanState)) ChangeState(shmovementState); // reset to normal when already leaning
		else ChangeState(new LeanState(this.pivotTransform, false)); // Lean left
	}

	void OnCrouch() {
		if (currentState == crouchState) ChangeState(shmovementState);
		else ChangeState(crouchState);
    }
}
