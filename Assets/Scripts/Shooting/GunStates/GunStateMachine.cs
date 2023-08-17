using UnityEngine;

public class GunStateMachine {
    // Character
    Character character;

    // States
    public IGunState currentState;
    public static IGunState ADSState;
    public static IGunState HipState;
    
    // Fields to pass down to states
    public Transform ADSTransform;
    public Transform gunTransform;
    public Transform hipTransform;

    public GunStateMachine(Transform ADSTransform, Transform gunTransform, Transform hipTransform, Character character) {
        this.character = character;
        this.ADSTransform = ADSTransform;
        this.gunTransform = gunTransform;
        this.hipTransform = hipTransform;
        HipState = new HipState(hipTransform);
        ADSState = new ADSState(ADSTransform);
        currentState = HipState;
        currentState.OnEnterState(character);
    }

    public void ChangeState(IGunState newState) {
        currentState.OnExitState(character);
        currentState = newState;
        newState.OnEnterState(character);
    }
}


