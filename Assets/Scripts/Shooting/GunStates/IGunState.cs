public interface IGunState {
    public abstract void Tick(Character character);
    public abstract void OnEnterState(Character character);
    public abstract void OnExitState(Character character);
}
