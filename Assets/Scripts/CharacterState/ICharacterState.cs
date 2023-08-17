public interface ICharacterState {
    public void Tick(Character character);
    public void OnEnterState(Character character);
    public void OnExitState(Character character);
}
