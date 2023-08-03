using UnityEngine;

public class MovementState : ICharacterState {
    float acceleration = 10f;

    public void OnEnterState(GameObject character) { }
    public void OnExitState(GameObject character) { }
    public void Tick(GameObject character) {
        this.SetVelocity(character);
        this.Jump(character);
    }
    public void Jump(GameObject character) {
        CharacterStateMachine csm = character.GetComponent<CharacterStateMachine>();
        float isJumping = csm.jump.action.ReadValue<float>();
        Debug.Log("IsJumping: " + isJumping);
        if (isJumping > .9f) csm.ChangeState(new JumpState());
    }
    public void SetVelocity(GameObject character) {
        Vector3 direction = Vector3.forward; //TODO change to get input
        Rigidbody rb = character.GetComponent<Rigidbody>();

        if (direction == Vector3.zero) {
            // If there is no input
            if (rb.velocity.magnitude < .1f) {
                rb.velocity = Vector3.zero;
                return;
            }
            else {
                Vector3 currentV = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
                Vector3 deceleration = -(rb.velocity.normalized * Time.deltaTime * acceleration * 4);
                rb.velocity += deceleration;
                if (Vector3.Dot(currentV, rb.velocity) < 0) {
                    //We would decelerate into the opposite direction. just stop.
                    rb.velocity = Vector3.zero;
                }
            }
        }
        else if (Vector3.Dot(direction, rb.velocity) < 0f) {
            rb.velocity -= rb.velocity.normalized * Time.deltaTime * acceleration * 4;
        }
        else {
            //otherwise, check the angle between the intended direction and our velocity
            Vector3 dampenVector;
            if (Vector3.SignedAngle(rb.velocity, direction, Vector3.up) > 0f) {
                //if the angle is greater than zero, we're trying to turn right.
                //dampen our velocity at an angle 90 degrees to the right of our intended direction
                dampenVector = Quaternion.AngleAxis(90f, Vector3.up) * rb.velocity;
            }
            else {
                // if its less than zero, we're trying to turn left
                //dampen our velocity at an angle 90 degrees to the left of our intended direction
                dampenVector = Quaternion.AngleAxis(-90f, Vector3.up) * rb.velocity;
            }
            rb.velocity += dampenVector.normalized * Time.deltaTime * acceleration * 4;
        }

        rb.velocity += direction * Time.deltaTime * acceleration;
    }
}
