using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform look;
	[Header("Player Movement")]
	[Tooltip("Move speed of the player in m/s")]
    [SerializeField] private float moveSpeed = 4.0f;
	[Tooltip("Sprint speed of the player in m/s")]
    [SerializeField] private float sprintSpeed = 6.0f;
	[Tooltip("Rotation speed of the player")]
    [SerializeField] private float rotationSpeed = 1.0f;

	[Space(10)]
	[Tooltip("The height the player can jump")]
    [SerializeField] private float jumpHeight = 1.2f;
	[Tooltip("The player uses its own gravity value. The engine default is -9.81f")]
	[SerializeField] private float gravity = -15.0f;
    [SerializeField] private float terminalVelocity = 53.0f;

	[Header("Player Grounded")]
	[Tooltip("If the player is isGrounded or not. Not part of the CharacterController built in isGrounded check")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundedOffset = 0.85f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

	[Header("Camera Limits")]
	[SerializeField] private float minCameraAngle = -90F;
    [SerializeField] private float maxCameraAngle = 90F;

	[Header("Channels")]
    [SerializeField] private BoolChanel isTriggerEvent;
    [SerializeField] private Vector2Channel directionEvent;
    [SerializeField] private Vector2Channel lookEvent;
    [SerializeField] private EmptyAction jumpEvent;
    [SerializeField] private BoolChanel sprintEvent;
    [SerializeField] private EmptyAction reloadEvent;
    [SerializeField] private EmptyAction interactEvent;

    private CharacterController controller;

	private Quaternion characterTargetRot;
	private Quaternion cameraTargetRot;

	private float verticalVelocity;

	private bool sprint;
	private bool _jump;
	private Vector2 _direction;
	private Vector2 _lookRotation;

    private void OnEnable()
    {
		directionEvent.Subscribe(HandleDirection);
		lookEvent.Subscribe(HandleLook);
		jumpEvent.Subscribe(HandleJump);
		sprintEvent.Subscribe(HandleSprint);
    }

    private void OnDisable()
    {
        directionEvent.Unsubscribe(HandleDirection);
        lookEvent.Unsubscribe(HandleLook);
		jumpEvent.Unsubscribe(HandleJump);
        sprintEvent.Unsubscribe(HandleSprint);
    }

    private void Awake()
    {
		if (!look)
		{
            Debug.LogError($"{name}: Look is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
		if (groundLayers.value == 0)
		{
            Debug.LogError($"{name}: Select a LayerMask.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Start()
	{
		controller = GetComponent<CharacterController>();
		characterTargetRot = transform.localRotation;
		cameraTargetRot = look.localRotation;
		UnityEngine.Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		GroundedCheck();
		JumpAndGravity();
		LookRotation();
		Move();
    }

	private void GroundedCheck()
	{
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
		isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
	}

	private void JumpAndGravity()
	{
		if (isGrounded && _jump)
		{
			verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
		else
		{
			_jump = false;
		}

		if (verticalVelocity < terminalVelocity)
			verticalVelocity += gravity * Time.deltaTime;
	}

    private void Move()
	{
        Vector3 moveValue = new Vector3(_direction.x, 0, _direction.y);

        moveValue = moveValue.x * transform.right + moveValue.z * transform.forward;

        float targetSpeed = sprint ? sprintSpeed : moveSpeed;

		controller.Move(moveValue.normalized * (targetSpeed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void LookRotation()
	{
		float yRot = _lookRotation.x * rotationSpeed;
		float xRot = _lookRotation.y * rotationSpeed;

		characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		cameraTargetRot = ClampRotationAroundXAxis(cameraTargetRot);

		transform.localRotation = characterTargetRot;
		look.localRotation = cameraTargetRot;
	}

	private Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
		angleX = Mathf.Clamp(angleX, minCameraAngle, maxCameraAngle);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	private void HandleJump()
	{
		_jump = true;
	}

	private void HandleDirection(Vector2 dir)
	{
		_direction = dir;
	}

	private void HandleLook(Vector2 dir)
	{
		_lookRotation = dir;
	}

	private void HandleSprint(bool _sprint)
	{
		sprint = _sprint;
	}
}
