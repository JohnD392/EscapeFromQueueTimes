using Mirror;
using UnityEngine;

public class Character : NetworkBehaviour {
	CharacterStateMachine csm;
	LeanStateMachine lsm;
	GunStateMachine gsm;

	public Transform ADSTransform;
	public Transform gunTransform;
	public Transform hipTransform;

	public Transform basePivotTransform;
	public Transform pivotTransform;
	public Transform cameraTransform;

	public delegate void ADSEventHandler();
	public static event ADSEventHandler OnADS;
	public delegate void StopADSEventHandler();
	public static event StopADSEventHandler OnStopADS;
	public delegate void LeanEventHandler();
	public static event LeanEventHandler OnLean;
	public delegate void DeathEventHandler();
	public static event DeathEventHandler OnDeath;

    public void Start() {
		csm = new CharacterStateMachine(basePivotTransform, pivotTransform, this);
		lsm = new LeanStateMachine(pivotTransform, this);
		gsm = new GunStateMachine(ADSTransform, gunTransform, hipTransform, this);
		Debug.Log("Start gsm: " + gsm);
    }

    public void FixedUpdate() {
		if (isLocalPlayer) {
			csm.currentState.Tick(this);
			lsm.currentState.Tick(this);
			gsm.currentState.Tick(this);
		}
	}

	public void ADS() {
		OnADS();
	}

	public void StopADS() {
		OnStopADS();
	}

	void OnJump() {
		csm.ChangeState(CharacterStateMachine.jumpState);
	}

	void OnLeanRight() {
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

	//Triggered by inputsystem via SendMessage()
	void OnAim() {
		if (gsm.currentState == GunStateMachine.ADSState) gsm.ChangeState(GunStateMachine.HipState);
		else if (gsm.currentState == GunStateMachine.HipState) gsm.ChangeState(GunStateMachine.ADSState);
	}


	//void OnLeanRight() {
	//	if (isLeaning) {
	//		isLeaning = false;
	//		GetComponent<LeanScript>().Lean(LeanScript.Direction.Straight);
	//	}
	//	else {
	//		isLeaning = true;
	//		GetComponent<LeanScript>().Lean(LeanScript.Direction.Right);
	//	}
	//}

	//void OnLeanLeft() {
	//	if (isLeaning) {
	//		isLeaning = false;
	//		GetComponent<LeanScript>().Lean(LeanScript.Direction.Straight);
	//	}
	//	else {
	//		isLeaning = true;
	//		GetComponent<LeanScript>().Lean(LeanScript.Direction.Left);
	//	}
	//}

	void OnCrouch() {
		if (csm.currentState == CharacterStateMachine.crouchState) csm.ChangeState(CharacterStateMachine.shmovementState);
		else csm.ChangeState(CharacterStateMachine.crouchState);
	}
}