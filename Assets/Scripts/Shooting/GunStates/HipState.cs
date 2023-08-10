using UnityEngine;
public class HipState : IGunState {
    public Transform hipPosition;

    public HipState(Transform hipPosition) {
        this.hipPosition = hipPosition;
    }

    public void OnEnterState(GameObject character) {
        GunStateMachine gsm = character.GetComponent<GunStateMachine>();
        gsm.gunTransform.position = gsm.hipTransform.position;
        gsm.gunTransform.rotation = gsm.hipTransform.rotation;
    }

    public void OnExitState(GameObject character) { }

    public void Tick(GameObject character) { }
}

