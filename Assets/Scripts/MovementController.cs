using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 8f;
	[SerializeField] float lookSensitivity = 0.1f;
	[SerializeField] float jumpForce = 25f;

	Rigidbody rigidBody;
	Camera playerCamera;

	Vector2 moveInputVector;
	Vector2 lookInputVector;

	bool isGrounded = true;

	public Vector3 ClampViewAngle(Transform playerCamera)
	{
		if (playerCamera.up.y < 0f)
		{
			if (playerCamera.forward.y > 0f)
			{
				return new Vector3(270f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
			}
			return new Vector3(90f, playerCamera.localEulerAngles.y, playerCamera.localEulerAngles.z);
		}

		return playerCamera.localEulerAngles;
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rigidBody = GetComponent<Rigidbody>();
		playerCamera = GetComponentInChildren<Camera>();
	}

	void Update()
	{
		Move();
	}

	private void LateUpdate()
	{
		Look();
	}

	void Move()
	{
		if (isGrounded)
		{
			Vector3 inputDirection = new Vector3(moveInputVector.x, 0, moveInputVector.y);      // input direction
			Vector3 worldSceneDirection = transform.TransformDirection(inputDirection);         // world space direction

			float velocityX = worldSceneDirection.x * moveSpeed;
			float velocityY = rigidBody.velocity.y;
			float velocityZ = worldSceneDirection.z * moveSpeed;

			rigidBody.velocity = new Vector3(velocityX, velocityY, velocityZ);
		}
	}

	void Look()
	{
		Vector2 delta = lookInputVector * lookSensitivity;
		playerCamera.transform.Rotate(-delta.y, 0f, 0f);
		playerCamera.transform.localEulerAngles = ClampViewAngle(playerCamera.transform);
		Quaternion rigidRot = rigidBody.rotation;
		Quaternion deltaRot = Quaternion.Euler(0f, delta.x, 0f);
		rigidBody.MoveRotation(rigidRot * deltaRot);
	}

	void Jump()
	{
		if (isGrounded)
		{
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
			isGrounded = false;
		}
	}

	// Called by InputSystem on the Player prefab via SendMessage()
	void OnMove(InputValue value)
	{
		moveInputVector = value.Get<Vector2>();
	}

	// Called by InputSystem on the Player prefab via SendMessage()
	void OnLook(InputValue value)
	{
		lookInputVector = value.Get<Vector2>();
	}

	// Called by InputSystem on the Player prefab via SendMessage()
	void OnJump(InputValue value)
	{
		if (value.isPressed)
		{
			Jump();
		}
	}

	// Called when an attached Collider contacts another Collider
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.transform.tag == "Ground")
		{
			isGrounded = true;
		}
	}
}
