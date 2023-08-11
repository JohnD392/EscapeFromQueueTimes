using System.Collections;
using UnityEngine;

public class LeanState : ICharacterState {
    Transform pivotTransform;
    MovementState movementState;
    float maxDeg;
    float leanSpeed = 10f;
    bool right;

    public LeanState(Transform pivotTransform, bool right) {
        this.right = right;
        maxDeg = 22f;
        if (right) maxDeg = -maxDeg;
        this.pivotTransform = pivotTransform;
    }

    public void OnEnterState(GameObject character) {
        movementState = new MovementState(2.5f);
    }

    public void OnExitState(GameObject character) {
        character.GetComponent<CharacterStateMachine>().StopCoroutine("LeanCoroutine");
    }

    public void Tick(GameObject character) {
        movementState.Tick(character);
    }

    float GetPivotAngle() {
        return pivotTransform.rotation.eulerAngles.z;
    }
}