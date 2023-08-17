using UnityEngine;

public class CharacterStateMachine {
	Character character;

	public ICharacterState currentState;

	public Transform basePivotTransform;
	public Transform pivotTransform;
	public Transform cameraTransform;

	public static ICharacterState jumpState;
	public static ICharacterState shmovementState;
	public static ICharacterState crouchState;


	public CharacterStateMachine(Transform basePivotTransform, Transform pivotTransform, Character character) {
		this.basePivotTransform = basePivotTransform;
		this.pivotTransform = pivotTransform;
		this.character = character;

		jumpState = new JumpState();
		shmovementState = new MovementState();
		crouchState = new CrouchState(basePivotTransform, pivotTransform);

		Initialize(shmovementState);
	}

	public void Initialize(ICharacterState startingState) {
		currentState = startingState;
		startingState.OnEnterState(character);
	}

	public void ChangeState(ICharacterState newState) {
		currentState.OnExitState(character);
		currentState = newState;
		newState.OnEnterState(character);
	}

	public bool InState(System.Type type) {
		if (currentState.GetType() == type) return true;
		return false;
	}
}
