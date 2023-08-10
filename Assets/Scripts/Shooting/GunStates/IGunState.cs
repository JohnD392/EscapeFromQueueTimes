using UnityEngine;

public interface IGunState {
    public void Tick(GameObject character);
    public void OnEnterState(GameObject character);
    public void OnExitState(GameObject character);
}
