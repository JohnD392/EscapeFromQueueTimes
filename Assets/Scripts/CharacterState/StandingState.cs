using UnityEngine;

public class StandingState : ICharacterState {
    private Character character;
    private Rigidbody rb;
    float maxSpeed;

    public StandingState(Character character) {
        this.character = character;
        this.maxSpeed = character.maxSpeed;
    }

    public virtual void OnEnterState(Character character) {
        rb = character.GetComponent<Rigidbody>();
    }

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
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (direction == Vector3.zero) {
            // Player is not issuing movement input
            // Only apply deceleration to the player
            Vector3 decelerationVec = -horizontalVelocity.normalized * character.deceleration;
            // If the deceleration vector would flip the direction of movement, just set speed to zero
            if (decelerationVec.magnitude > horizontalVelocity.magnitude) {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                return;
            }
            // If not, decelerate normally
            rb.velocity += decelerationVec;
            return;
        }
        // Player is issuing movement input
        rb.velocity += inputVector * character.acceleration;
    }

    bool HasDirectionalInput(Vector3 inputVector) {
        return Vector3.Distance(inputVector, Vector3.zero) > .1f;
    }

    float CurrentFallSpeed() {
        return character.GetComponent<Rigidbody>().velocity.y;
    }

    public static void SpeedLimit(Rigidbody rb, float maxSpeed) {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void CheckSwapStates() {
        if(!character.IsGrounded()) character.GetComponent<PostureStateMachine>().ChangeState(PostureStateMachine.jumpState);
    }
}
