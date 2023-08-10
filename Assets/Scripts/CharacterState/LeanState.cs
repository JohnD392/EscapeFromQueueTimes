using System.Collections;
using UnityEngine;

public class LeanState : ICharacterState {
    Transform pivotPoint;
    float leanTime = .23f;
    float maxDeg;
    float animationTimer = 0f;
    MovementState movementState;

    public LeanState(Transform pivotPoint, bool direction) {
        maxDeg = 30f;
        if (direction) maxDeg = -maxDeg;
        this.pivotPoint = pivotPoint;
    }

    public void OnEnterState(GameObject character) {
        animationTimer = 0f;
        movementState = new MovementState();
        character.GetComponent<CharacterStateMachine>().StartCoroutine(LeanCoroutine(character, pivotPoint.rotation, character.transform.rotation * Quaternion.Euler(0f, 0f, maxDeg)));
    }

    public void OnExitState(GameObject character) {
        character.GetComponent<CharacterStateMachine>().StopCoroutine("LeanCoroutine");
        animationTimer = 0f;
        character.GetComponent<CharacterStateMachine>().StartCoroutine(LeanCoroutine(character, pivotPoint.rotation, character.transform.rotation));
    }

    public void Tick(GameObject character) {
        movementState.Tick(character);
    }

    IEnumerator LeanCoroutine(GameObject character, Quaternion startRotation, Quaternion targetRotation) {
        while (animationTimer < leanTime) {
            animationTimer += Time.unscaledDeltaTime;
            pivotPoint.rotation = Quaternion.Lerp(startRotation, targetRotation, animationTimer / leanTime);
            yield return new WaitForEndOfFrame();
        }
        pivotPoint.rotation = targetRotation;
        yield return null;
    }
}