using UnityEngine;

public class ADSState : IGunState {
    public Transform ADSTransform;

    public ADSState(Transform ADSTransform) {
        this.ADSTransform = ADSTransform;
    }

    public void OnEnterState(Character character) {
        character.gunTransform.position = ADSTransform.position;
        character.gunTransform.rotation = ADSTransform.rotation;
    }
    public void OnExitState(Character character) { }
    public void Tick(Character character) { }
}
