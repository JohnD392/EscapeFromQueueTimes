using UnityEngine;

public class LeanStateMachine {
    public ICharacterState currentState;

    public static ICharacterState LeftLeanState;
    public static ICharacterState RightLeanState;
    public static ICharacterState StraightLeanState;

    public Character character;

    public LeanStateMachine(Transform pivotTransform, Character character) {
        this.character = character;
        LeftLeanState = new LeftLeanState(pivotTransform, character);
        RightLeanState = new RightLeanState(pivotTransform, character);
        StraightLeanState = new StraightLeanState(pivotTransform, character);

        this.currentState = StraightLeanState;
        this.currentState.OnEnterState(character);
    }

     public void ChangeState(ICharacterState newState) {
        this.currentState.OnExitState(character);
        this.currentState = newState;
        newState.OnEnterState(character);
    }
}

public class LeftLeanState : ICharacterState {
    Transform pivotTransform;
    float targetAngle = 22f;
    float leanSpeed = 100f;
    float currentAngle = 0f;

    public LeftLeanState(Transform pivotTransform, Character character) {
        this.pivotTransform = pivotTransform;
    }
    
    public void OnExitState(Character character) { }

    public void OnEnterState(Character character) {
        currentAngle = pivotTransform.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
    }

    public void Tick(Character character) {
        // Potential bug if we somehow lean too fast? Abs may always be > 1f
        if (Mathf.Abs(currentAngle - targetAngle) > 1f) {
            float rotationAmount = Time.unscaledDeltaTime * leanSpeed * Mathf.Sign(targetAngle - currentAngle);
            currentAngle += rotationAmount;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        } else {
            currentAngle = targetAngle;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        }
    }
}

public class RightLeanState : ICharacterState {
    Transform pivotTransform;
    float targetAngle = -22f;
    float leanSpeed = 100f;
    float currentAngle = 0f;

    public RightLeanState(Transform pivotTransform, Character character) {
        this.pivotTransform = pivotTransform;
    }

    public void OnExitState(Character character) { }

    public void OnEnterState(Character character) {
        currentAngle = pivotTransform.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
    }

    public void Tick(Character character) {
        // Potential bug if we somehow lean too fast? Abs may always be > 1f
        if (Mathf.Abs(currentAngle - targetAngle) > 1f) {
            float rotationAmount = Time.unscaledDeltaTime * leanSpeed * Mathf.Sign(targetAngle - currentAngle);
            currentAngle += rotationAmount;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        }
        else {
            currentAngle = targetAngle;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        }
    }
}

public class StraightLeanState : ICharacterState {
    Transform pivotTransform;
    float targetAngle = 0;
    float leanSpeed = 100f;
    float currentAngle = 0f;

    public StraightLeanState(Transform pivotTransform, Character character) {
        this.pivotTransform = pivotTransform;
    }

    public void OnExitState(Character character) { }

    public void OnEnterState(Character character) {
        currentAngle = pivotTransform.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
    }

    public void Tick(Character character) {
        // Potential bug if we somehow lean too fast? Abs may always be > 1f
        if (Mathf.Abs(currentAngle - targetAngle) > 1f) {
            float rotationAmount = Time.unscaledDeltaTime * leanSpeed * Mathf.Sign(targetAngle - currentAngle);
            currentAngle += rotationAmount;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        }
        else {
            currentAngle = targetAngle;
            pivotTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        }
    }
}
