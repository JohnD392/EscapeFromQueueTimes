using UnityEngine;

public class StandingState : ICharacterState {
    private Character character;
    float maxSpeed;

    public StandingState(Character character) {
        this.character = character;
        this.maxSpeed = character.maxSpeed;
    }

    public virtual void OnEnterState(Character character) { }
    public virtual void OnExitState(Character character) { }
    public virtual void Tick(Character character) {
        CheckADS();
        Vector2 moveVec = character.GetComponent<PlayerInputReader>().moveVec;
        Vector3 moveInput = new Vector3(moveVec.x, 0f, moveVec.y);
        SetVelocity(character, character.transform.TransformDirection(moveInput));
        SpeedLimit(character.GetComponent<Rigidbody>(), maxSpeed);
    }
    
    void SlowForADS() {
        this.maxSpeed = character.maxSpeed / 1.5f;
    }
    void StopSlowForADS() {
        this.maxSpeed = character.maxSpeed;
    }
    private void CheckADS() {
        if (character.gsm.currentState == GunStateMachine.ADSState) SlowForADS();
        else StopSlowForADS();
    }
    public void SetVelocity(Character character, Vector3 inputVector) {
        Vector3 direction = inputVector.normalized;
        Rigidbody rb = character.GetComponent<Rigidbody>();
        Vector3 intendedVelocity = direction * maxSpeed;
        if(Vector3.Distance(inputVector, Vector3.zero) < .05f) {
            // The player has no input. Decelerate
            rb.velocity += -rb.velocity * character.deceleration * Time.deltaTime;
        } else {
            rb.velocity += intendedVelocity * character.acceleration;
        }
        rb.velocity = new Vector3(rb.velocity.x, CurrentFallSpeed(), rb.velocity.z);
    }

    private float CurrentFallSpeed() {
        return character.GetComponent<Rigidbody>().velocity.y;
    }

    public static void SpeedLimit(Rigidbody rb, float maxSpeed) {
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    public void CheckSwapStates() {
        if(!character.IsGrounded()) character.GetComponent<PostureStateMachine>().ChangeState(PostureStateMachine.jumpState);
    }
}
