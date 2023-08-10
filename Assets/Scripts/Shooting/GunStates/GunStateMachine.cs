using UnityEngine;

public class GunStateMachine : MonoBehaviour {
    public IGunState currentState;

    public Transform ADSTransform;
    public Transform gunTransform;
    public Transform hipTransform;

    public IGunState ADSState;
    public IGunState HipState;

    public void Start() {
        this.HipState = new HipState(hipTransform);
        this.ADSState = new ADSState(ADSTransform);
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


