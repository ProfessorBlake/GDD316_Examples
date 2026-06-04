using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.AnimationLayers
{
    public class Player : MonoBehaviour
    {
		// Generate integers by hashing strings when compiled
		private static readonly int AniIsCarrying = Animator.StringToHash("IsCarrying");
		private static readonly int AniMoveSpeed = Animator.StringToHash("MoveSpeed");
		private static readonly int AniBreathSpeed = Animator.StringToHash("Breathing");
		private static readonly int AniInjury = Animator.StringToHash("Injury");

		// Events
		public Action<float> OnMoveSpeedUpdate;
		public Action<float> OnTiredUpdate;
		public Action<float> OnCarryUpdate;
		public Action<float> OnInjuryUpdate;

		[Header("Animation")]
		[SerializeField] private Animator animator;
		[SerializeField] private float breathingChangeSpeed;

		private bool isCarryingEquipment;
		private float breathingRate;

		[Header("Movement")]
		[SerializeField] private float walkSpeed;
		[SerializeField] private float runSpeed;
		[SerializeField] private GameObject equipmentInHand;

		private PlayerControls controls;
		private float velocity;
		private float injuryValue;

		[Header("Collisions")]
		[SerializeField] private Vector3 collisionPointOffset;
		[SerializeField] private float collisionRadius;

		private Collider[] hits = new Collider[2];

		private void Awake()
		{
			controls = new PlayerControls();
		}

		private void OnEnable()
		{
			controls.Enable();
		}

		private void OnDisable()
		{
			controls.Disable();
		}

		private void Update()
		{
			// Get move speed
			float move = controls.Player.Move.ReadValue<Vector2>().x;
			bool sprint = controls.Player.Sprint.IsPressed();
			move *= (sprint ? runSpeed : walkSpeed);

			// Move
			float tiredMod = 1f;
			if (breathingRate > runSpeed * 0.9f)
				tiredMod = 0.7f;
			else if (breathingRate > runSpeed * 0.6f)
				tiredMod = 0.85f;
			
			velocity = Mathf.MoveTowards(velocity, move * tiredMod, 3 * Time.deltaTime); // Slow when tired;
			transform.position += new Vector3(0f, 0f, velocity * Time.deltaTime);

			// Move speed animation
			animator.SetFloat(AniMoveSpeed, velocity / runSpeed); //Send speed as percetage of max for blendtree

			// Breathing animation
			breathingRate = Mathf.MoveTowards(breathingRate, move, breathingChangeSpeed * (isCarryingEquipment && (move > walkSpeed || move <= 0.01f) ? 2f : 1f) * Time.deltaTime);
			animator.SetFloat(AniBreathSpeed, breathingRate/runSpeed);//Send speed as percetage of max for blendtree

			// Dampen injury
			if (injuryValue > 0f)
			{
				injuryValue = Mathf.MoveTowards(injuryValue, 0, 0.1f * Time.deltaTime);
				animator.SetFloat(AniInjury, injuryValue / 0.5f);//Send as percetage
			}

			//Update subscribers
			OnMoveSpeedUpdate?.Invoke(velocity / runSpeed);
			OnTiredUpdate?.Invoke(breathingRate / runSpeed);
			OnInjuryUpdate?.Invoke(injuryValue / 0.5f);

			// Check collisions
			if (!isCarryingEquipment)
			{
				if (Physics.OverlapSphereNonAlloc(transform.position + collisionPointOffset, collisionRadius, hits) > 0)
				{
					TogglePickupEquipment(true);
				}
			}
		}

		private void TogglePickupEquipment(bool pickedUp)
		{
			isCarryingEquipment = pickedUp;
			hits[0].gameObject.SetActive(!pickedUp);
			equipmentInHand.SetActive(pickedUp);
			animator.SetBool(AniIsCarrying, pickedUp);
			OnCarryUpdate?.Invoke(pickedUp ? 1 : 0);
		}

		public void TriggerInjury()
		{
			injuryValue = 0.5f;
			CameraShake cam = FindAnyObjectByType<CameraShake>();
			if (cam != null)
			{
				cam.Shake(1f);
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position + collisionPointOffset, collisionRadius);
		}
	}
}
