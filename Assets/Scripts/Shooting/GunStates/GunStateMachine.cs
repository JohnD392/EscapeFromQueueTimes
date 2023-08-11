using UnityEngine;

public class GunStateMachine : MonoBehaviour {
    // States
    public IGunState currentState;
    public IGunState ADSState;
    public IGunState HipState;
    
    // Fields to pass down to states
    public Transform ADSTransform;
    public Transform gunTransform;
    public Transform hipTransform;

    public void Start() {
        HipState = new HipState(hipTransform);
        ADSState = new ADSState(ADSTransform);
        currentState = HipState;
        currentState.OnEnterState(gameObject);
    }

    public void ChangeState(IGunState newState) {
        currentState.OnExitState(gameObject);
        currentState = newState;
        newState.OnEnterState(gameObject);
    }

    //Triggered by inputsystem via SendMessage()
    void OnAim() {
        if(currentState == ADSState) ChangeState(HipState);
        else if (currentState == HipState) ChangeState(ADSState);
    }
}


