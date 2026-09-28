using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Composition
{
	[RequireComponent(typeof(PlayerInput))]
	public class ActionController : MonoBehaviour
    {
		private PlayerInput input;				// PlayerInput component
		[SerializeField] private IPlayerUsable[] equipment;      // Any attached equipment/spells
		private int activeEquipment = 0;		// Change this to use other equipment

		private void Awake()
		{
			input = GetComponent<PlayerInput>();
			equipment = GetComponents<IPlayerUsable>();	// Cache all usables
			Debug.Log(equipment.Length);
		}

		private void OnEnable()
		{
			if (input != null)
			{
				InputAction fire = input.actions.FindAction("Fire");	// Find Fire InputAction
				fire.performed += HandleUseInput;						// Subscribe to Fire action
				fire.canceled += HandleUseInput;
			}
		}

		private void OnDisable()
		{
			if (input != null)
			{
				InputAction fire = input.actions.FindAction("Fire");
				fire.performed -= HandleUseInput;                       // Unsubscribe
				fire.canceled -= HandleUseInput;
			}
		}

		private void HandleUseInput(InputAction.CallbackContext context)
		{
			if(context.phase == InputActionPhase.Performed)
			{
				equipment[activeEquipment].BeginUse();
			}
			else if(context.phase == InputActionPhase.Canceled)
			{
				equipment[activeEquipment].EndUse();
			}
		}
	}
}
