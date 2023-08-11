using UnityEngine;
using System.Collections;

public class LeanState : MovementState {
    Transform pivotTransform;
    float maxDeg;
    float leanMoveSpeed = 2.4f;
    float leanSpeed = 100f;
    float currentAngle = 0f;
    Coroutine currentCoroutine;

    public LeanState(Transform pivotTransform, bool right) : base() {
        this.pivotTransform = pivotTransform;
        maxDeg = 22f;
        if (right) maxDeg = -maxDeg;
    }

    public override void OnEnterState(GameObject character) {
        maxSpeed = leanMoveSpeed;
        currentAngle = pivotTransform.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
        currentCoroutine = character.GetComponent<CharacterStateMachine>().StartCoroutine(LeanCoroutine(maxDeg));
    }

    public override void OnExitState(GameObject character) {
        character.GetComponent<CharacterStateMachine>().StopCoroutine(currentCoroutine);
        currentCoroutine = character.GetComponent<CharacterStateMachine>().StartCoroutine(LeanCoroutine(0f));
    }

    IEnumerator LeanCoroutine(float targetAngle) {
        // Potential bug if we somehow lean too fast? Abs may always be > 1f
        while (Mathf.Abs(currentAngle - targetAngle) > 1f ) {
            float rotationAmount = Time.unscaledDeltaTime * leanSpeed * Mathf.Sign(targetAngle-currentAngle);
            currentAngle += rotationAmount;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
            yield return new WaitForEndOfFrame();
        }
        currentAngle = targetAngle;
        pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        yield return null;
    }
}
