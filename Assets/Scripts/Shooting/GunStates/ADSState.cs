using UnityEngine;

public class ADSState : IGunState {
    public Transform ADSTransform;

    public ADSState(Transform ADSTransform) {
        this.ADSTransform = ADSTransform;
    }

    public void OnEnterState(Character character) {
        character.gunTransform.position = ADSTransform.position;
        character.gunTransform.rotation = ADSTransform.rotation;
        character.ADS();
    }

    public void OnExitState(Character character) {
        character.StopADS();
    }

    public void Tick(Character character) { }
}
