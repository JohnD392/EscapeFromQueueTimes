using UnityEngine;

public interface IGunState {
    public abstract void Tick(GameObject character);
    public abstract void OnEnterState(GameObject character);
    public abstract void OnExitState(GameObject character);
}
