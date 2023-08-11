using UnityEngine;

public class ADSState : IGunState {
    public Transform ADSTransform;

    public ADSState(Transform ADSTransform) {
        this.ADSTransform = ADSTransform;
    }

    public void OnEnterState(GameObject character) {
        GunStateMachine gsm = character.GetComponent<GunStateMachine>();
        gsm.gunTransform.position = ADSTransform.position;
        gsm.gunTransform.rotation = ADSTransform.rotation;
    }

    public void OnExitState(GameObject character) { }

    public void Tick(GameObject character) { }
}

