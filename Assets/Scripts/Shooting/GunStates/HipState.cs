using UnityEngine;
public class HipState : IGunState {
    public Transform hipPosition;

    public HipState(Transform hipPosition) {
        this.hipPosition = hipPosition;
    }

    public void OnEnterState(Character character) {
        character.gunTransform.position = hipPosition.position;
        character.gunTransform.rotation = hipPosition.rotation;
    }

    public void OnExitState(Character character) { }

    public void Tick(Character character) { }
}

