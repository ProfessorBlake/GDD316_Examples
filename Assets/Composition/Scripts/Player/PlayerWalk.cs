using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Composition
{
	[RequireComponent(typeof(PlayerInput))]
    public class PlayerWalk : MonoBehaviour
    {
		[SerializeField] private float walkSpeed;

		[SerializeField] private float t;

		private PlayerInput input;
		private Vector2 moveInput;

		private void Awake()
		{
			input = GetComponent<PlayerInput>();
		}

		private void OnEnable()
		{
			if (input != null)
			{
				InputAction walk = input.actions.FindAction("Move");    // Find Fire InputAction
				walk.performed += HandleWalkInput;
				walk.canceled += HandleWalkInput;
			}
		}

		private void OnDisable()
		{
			if (input != null)
			{
				InputAction walk = input.actions.FindAction("Move");
				walk.performed -= HandleWalkInput;
				walk.canceled -= HandleWalkInput;
			}
		}

		private void Update()
		{
			transform.position += new Vector3(moveInput.x * walkSpeed * Time.deltaTime, 0, 0);
		}

		private void HandleWalkInput(InputAction.CallbackContext context)
		{
			moveInput = context.ReadValue<Vector2>();
		}
	}
}
