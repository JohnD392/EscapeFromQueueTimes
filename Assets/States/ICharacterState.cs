using UnityEngine;

public interface ICharacterState {
    public void Tick(GameObject character);
    public void OnEnterState(GameObject character);
    public void OnExitState(GameObject character);
}