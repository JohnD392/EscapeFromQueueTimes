using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : NetworkBehaviour {
	// State Machines
	public PostureStateMachine psm;
	public LeanStateMachine lsm;
	public GunStateMachine gsm;

	// Transforms
	public Camera mainCamera;
	public Camera weaponCamera;
	
	public Transform ADSTransform;
	public Transform gunTransform;
	public Transform hipTransform;

	//Lean Transforms
	public Transform basePivotTransform;
	public Transform pivotTransform;

	public Transform groundedTransform; // The point on the character from which we detect the ground
	public float groundedRadius; // How far from the grounded transform we check for ground
	public LayerMask groundMask;

	// Movement values
	public float maxSpeed;
	public float acceleration;
	public float deceleration;

	public override void OnStartClient()
	{
		base.OnStartClient();
		if (!base.IsOwner) {
			GetComponent<Character>().enabled = false;
			GetComponent<PlayerInput>().enabled = false;
			GetComponent<PlayerInputReader>().enabled = false;
			GetComponent<MouseLook>().enabled = false;
			GetComponent<Shooting>().enabled = false;
			mainCamera.enabled = false;
			weaponCamera.enabled = false;
			GetComponentInChildren<AudioListener>().enabled = false;
		}
	}


	public void Start() {
		psm = new PostureStateMachine(this, basePivotTransform, pivotTransform);
		lsm = new LeanStateMachine(this, pivotTransform);
		gsm = new GunStateMachine(this, ADSTransform, gunTransform, hipTransform);
    }

    public void FixedUpdate() {
		if (base.IsOwner) {
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

    private void OnDrawGizmos() {
		Gizmos.DrawSphere(groundedTransform.position, groundedRadius);
    }

    public bool IsGrounded() {
		// Do not return true when the character is moving upwards. They can kinda fly that way.
		if(GetComponent<Rigidbody>().velocity.y > .1f) return false;
		return Physics.CheckSphere(groundedTransform.position, groundedRadius, groundMask);
    }
}