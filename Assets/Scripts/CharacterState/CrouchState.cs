using UnityEngine;

public class CrouchState : ICharacterState {
	Character character;
	float maxSpeed;
	float acceleration = 8f;
	Transform basePivotTransform;
	public Transform pivotTransform;

	public CrouchState(Character character, Transform basePivotTransform, Transform pivotTransform) {
		this.character = character;
		maxSpeed = 3f;
		this.basePivotTransform = basePivotTransform;
		this.pivotTransform = pivotTransform;
	}

	public void OnEnterState(Character character) {
		pivotTransform.position = basePivotTransform.position - Vector3.up * .5f;
	}

	public void OnExitState(Character character) {
		pivotTransform.position = basePivotTransform.position;
	}

	public void Tick(Character character) {
		CheckADS();
		Vector2 moveVec = character.GetComponent<PlayerInputReader>().moveVec;
		Vector3 moveInput = new Vector3(moveVec.x, 0f, moveVec.y);
		StandingState.SetVelocity(character.gameObject, character.transform.TransformDirection(moveInput), acceleration);
		StandingState.SpeedLimit(character.GetComponent<Rigidbody>(), maxSpeed);
	}

	public void CheckADS() {
		if (character.gsm.currentState == GunStateMachine.ADSState) {
			SlowForADS();
		} else {
			StopSlowForADS();
		}
	}

	void SlowForADS() {
		maxSpeed = 1.3f;
	}

	void StopSlowForADS() {
		maxSpeed = 2.1f;
	}
}