using UnityEngine;

public class CrouchState : ICharacterState {
	float maxSpeed;
	float acceleration = 8f;
	Transform basePivotTransform;
	public Transform pivotTransform;

	public CrouchState(Transform basePivotTransform, Transform pivotTransform) {
		maxSpeed = 3f;
		this.basePivotTransform = basePivotTransform;
		this.pivotTransform = pivotTransform;
		Character.OnADS += SlowForADS;
		Character.OnStopADS += StopSlowForADS;
	}

	public void OnEnterState(Character character) {
		pivotTransform.position = basePivotTransform.position - Vector3.up * .5f;
	}

	public void OnExitState(Character character) {
		pivotTransform.position = basePivotTransform.position;
	}

	public void Tick(Character character) {
		Vector2 moveVec = character.GetComponent<PlayerInputReader>().moveVec;
		Vector3 moveInput = new Vector3(moveVec.x, 0f, moveVec.y);
		MovementState.SetVelocity(character.gameObject, character.transform.TransformDirection(moveInput), acceleration);
		MovementState.SpeedLimit(character.GetComponent<Rigidbody>(), maxSpeed);
	}

	void SlowForADS() {
		maxSpeed = 1.3f;
	}

	void StopSlowForADS() {
		maxSpeed = 3f;
	}

}