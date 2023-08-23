using System.Collections;
using UnityEngine;

public class LeanScript : MonoBehaviour {
    public Transform pivotTransform;
    private Coroutine currentCoroutine;
    private float currentAngle = 0f;
    private float leanSpeed = 100f;
    private float leanDegrees = 22f;
    public enum Direction {
        Left,
        Right,
        Straight
    }

    public void Start() {
        currentAngle = 0f;
    }

    public void Lean(Direction direction) {
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentAngle = pivotTransform.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
        if (direction == Direction.Left) {
            currentCoroutine = StartCoroutine(LeanCoroutine(leanDegrees));
        }
        else if (direction == Direction.Right) {
            currentCoroutine = StartCoroutine(LeanCoroutine(-leanDegrees));
        }
        else {
            currentCoroutine = StartCoroutine(LeanCoroutine(0f));
        }
    }

    IEnumerator LeanCoroutine(float targetAngle) {
        // Potential bug if we somehow lean too fast? Abs may always be > 1fS
        while (Mathf.Abs(currentAngle - targetAngle) > 1f) {
            float rotationAmount = Time.unscaledDeltaTime * leanSpeed * Mathf.Sign(targetAngle - currentAngle);
            currentAngle += rotationAmount;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
            yield return new WaitForEndOfFrame();
        }
        currentAngle = targetAngle;
        pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        yield return null;
    }
}
