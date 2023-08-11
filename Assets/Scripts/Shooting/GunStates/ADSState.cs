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
        character.GetComponent<CharacterStateMachine>().ADS();
    }

    public void OnExitState(GameObject character) {
        character.GetComponent<CharacterStateMachine>().StopADS();
    }

    public void Tick(GameObject character) { }
}
