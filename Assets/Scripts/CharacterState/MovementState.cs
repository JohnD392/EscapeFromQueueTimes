using UnityEngine;

public class MovementState : ICharacterState {
    float acceleration = 10f;
    protected float maxSpeed;

    public MovementState() {
        this.maxSpeed = 4f;
        CharacterStateMachine.OnADS += SlowForADS; // Subscribe to OnADS event, so we can SlowForADS when OnADS happens
        CharacterStateMachine.OnStopADS += StopSlowForADS;
    }

    public MovementState(float maxSpeed) {
        this.maxSpeed = maxSpeed;
    }

    void SlowForADS() {
        this.maxSpeed = 2f;
    }
    void StopSlowForADS() {
        this.maxSpeed = 4f;
    }

    public virtual void OnEnterState(GameObject character) { }
    public virtual void OnExitState(GameObject character) { }
    
    public virtual void Tick(GameObject character) {
        Vector2 moveVec = character.GetComponent<PlayerInputReader>().moveVec;
        Vector3 moveInput = new Vector3(moveVec.x, 0f, moveVec.y);
        SetVelocity(character, character.transform.TransformDirection(moveInput), acceleration);
        SpeedLimit(character.GetComponent<Rigidbody>(), maxSpeed);
    }

    public static void SetVelocity(GameObject character, Vector3 inputVector, float acceleration) {
        Vector3 direction = inputVector.normalized;
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
    public static void SpeedLimit(Rigidbody rb, float maxSpeed) {
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
