using UnityEngine;

public class PostureStateMachine {
	Character character;

	public ICharacterState currentState;

	public Transform basePivotTransform;
	public Transform pivotTransform;
	public Transform cameraTransform;

	public static ICharacterState jumpState;
	public static ICharacterState standingState;
	public static ICharacterState crouchState;

	public PostureStateMachine(Character character, Transform basePivotTransform, Transform pivotTransform) {
		this.basePivotTransform = basePivotTransform;
		this.pivotTransform = pivotTransform;
		this.character = character;

		jumpState = new JumpState();
		standingState = new StandingState(character);
		crouchState = new CrouchState(character, basePivotTransform, pivotTransform);

		Initialize(standingState);
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
