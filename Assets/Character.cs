using Mirror;
using UnityEngine;

public class Character : NetworkBehaviour {
	public PostureStateMachine psm;
	public LeanStateMachine lsm;
	public GunStateMachine gsm;

	public Transform ADSTransform;
	public Transform gunTransform;
	public Transform hipTransform;

	public Transform basePivotTransform;
	public Transform pivotTransform;
	public Transform cameraTransform;

    public void Start() {
		psm = new PostureStateMachine(this, basePivotTransform, pivotTransform);
		lsm = new LeanStateMachine(this, pivotTransform);
		gsm = new GunStateMachine(this, ADSTransform, gunTransform, hipTransform);
    }

    public void FixedUpdate() {
		if (isLocalPlayer) {
			psm.currentState.Tick(this);
			lsm.currentState.Tick(this);
			gsm.currentState.Tick(this);
		}
	}

	void OnJump() {
		psm.ChangeState(PostureStateMachine.jumpState);
	}
	void OnLeanRight() {
		// No lean on prone
		//if(csm.currentState == CharacterStateMachine.)
		if (lsm.currentState != LeanStateMachine.StraightLeanState) {
			lsm.ChangeState(LeanStateMachine.StraightLeanState);
		} else {
			lsm.ChangeState(LeanStateMachine.RightLeanState);
		}
	}
	void OnLeanLeft() {
		if (lsm.currentState != LeanStateMachine.StraightLeanState) {
			lsm.ChangeState(LeanStateMachine.StraightLeanState);
		} else {
			lsm.ChangeState(LeanStateMachine.LeftLeanState);
        }
    }
	void OnAim() {
		if (gsm.currentState == GunStateMachine.ADSState) gsm.ChangeState(GunStateMachine.HipState);
		else if (gsm.currentState == GunStateMachine.HipState) gsm.ChangeState(GunStateMachine.ADSState);
	}
	void OnCrouch() {
		if (psm.currentState == PostureStateMachine.crouchState) psm.ChangeState(PostureStateMachine.standingState);
		else psm.ChangeState(PostureStateMachine.crouchState);
	}
}