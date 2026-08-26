using UnityEngine;

namespace Game.AnimationIK.Spider
{
	[RequireComponent(typeof(CharacterController))]
	public class IKMover : MonoBehaviour
	{
		private readonly int aniWalkSpeed = Animator.StringToHash("speed");

		[SerializeField] private float acceleration;
		[SerializeField] private float walkSpeed;
		[SerializeField] private float turnSpeed;
		[SerializeField] private float gravity;
		[SerializeField] private Animator animator;

		[Header("IK")]
		[SerializeField] private float legDistance;
		[SerializeField] private float ccHeightGrounded;
		[SerializeField] private float footHeelOffset;	// distance of heel from center of foot
		[SerializeField] private float footToeOffset;	// distance of toe from center of foot
		private Vector3 leftFootLockPos;   // position foot should stick to when making contact
		private Quaternion leftFoorLockRot;
		private Vector3 rightFootLockPos;   // position foot should stick to when making contact
		private Quaternion rightFootLockRot;
		private bool leftFootLocked;    // foot locked to ground
		private bool rightFootLocked;    // foot locked to ground
		private bool conformToTerrain;	// disable on stairs

		private CharacterController cc;
		private float yspeed;
		Vector3 move;
		private float heightTarget;

		private void Awake()
		{
			cc = GetComponent<CharacterController>();
		}

		private void Update()
		{
			Move();
			Animate();
		}

		private void Move()
		{
			move = (transform.forward * Mathf.Abs(Input.GetAxisRaw("Vertical")) * Time.deltaTime);

			if (cc.isGrounded)
			{
				yspeed = 0;
			}
			else
			{
				yspeed += gravity * Time.deltaTime;
			}
			cc.Move(move + new Vector3(0,yspeed,0) * Time.deltaTime);
		}

		private void Animate()
		{
			//animator.SetFloat(aniWalkSpeed, move.sqrMagnitude > 0f ? 1f : 0f);
			animator.speed = move.sqrMagnitude > 0f ? 1f : 0f;
			cc.height = Mathf.Lerp(cc.height, heightTarget, 20 * Time.deltaTime);	// Move CC height when stepping up/down
		}

		private void OnAnimatorIK(int layerIndex)
		{
			//============================
			/// LEFT FOOT
			///===========================
			
			// Set weights
			animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootIK"));
			animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootIK"));

			// heel/toe positions
			Vector3 leftHeelPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot) + transform.forward * footHeelOffset;
			Vector3 leftToePos = animator.GetIKPosition(AvatarIKGoal.LeftFoot) + transform.forward * footToeOffset;

			// editor visualization
			Debug.DrawLine(leftHeelPos + Vector3.up, leftHeelPos, Color.yellow);
			Debug.DrawLine(leftToePos + Vector3.up, leftToePos, Color.yellow);

			// Raycast both heel and toe
			bool hitHeelL = Physics.Raycast(leftHeelPos + Vector3.up, Vector3.down, out RaycastHit heelHitL, legDistance + 2f);
			bool hitToeL = Physics.Raycast(leftToePos + Vector3.up, Vector3.down, out RaycastHit toeHitL, legDistance + 2f);

			if(animator.GetFloat("LeftFootIK") <= 0.1f)
				leftFootLocked = false;

			if (hitHeelL && hitToeL)
			{
				// average y pos between heel/toe
				float avgHitY = (heelHitL.point.y + toeHitL.point.y) * 0.5f;
				Vector3 ikFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

				// get groundAngle
				Vector3 groundAngle = (toeHitL.point - heelHitL.point).normalized;

				// get rotation for foot
				Quaternion footPitchRotation = Quaternion.FromToRotation(transform.forward, groundAngle);

				// lock foot to position
				if (!leftFootLocked)
				{
					leftFootLocked = true;
					leftFootLockPos = ikFootPos;
					leftFoorLockRot = footPitchRotation;
				}

				// move ik pos
				animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftFootLockPos.x, avgHitY + legDistance, leftFootLockPos.z));

				// rotate to position
				animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoorLockRot * transform.rotation);
			}

			//============================
			/// Right FOOT
			///===========================

			// Set weights
			animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootIK"));
			animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootIK"));

			// heel/toe positions
			Vector3 rightHeelPos = animator.GetIKPosition(AvatarIKGoal.RightFoot) + transform.forward * footHeelOffset;
			Vector3 rightToePos = animator.GetIKPosition(AvatarIKGoal.RightFoot) + transform.forward * footToeOffset;

			// editor visualization
			Debug.DrawLine(rightHeelPos + Vector3.up, rightHeelPos, Color.yellow);
			Debug.DrawLine(rightToePos + Vector3.up, rightToePos, Color.yellow);

			// Raycast both heel and toe
			bool hitHeelR = Physics.Raycast(rightHeelPos + Vector3.up, Vector3.down, out RaycastHit heelHitR, legDistance + 2f);
			bool hitToeR = Physics.Raycast(rightToePos + Vector3.up, Vector3.down, out RaycastHit toeHitR, legDistance + 2f);

			if (animator.GetFloat("RightFootIK") <= 0.1f)
				rightFootLocked = false;

			if (hitHeelR && hitToeR)
			{
				// average y pos between heel/toe
				float avgHitY = (heelHitR.point.y + toeHitR.point.y) * 0.5f;
				Vector3 ikFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);

				// get groundAngle
				Vector3 groundAngle = (toeHitR.point - heelHitR.point).normalized;

				// get rotation for foot
				Quaternion footPitchRotation = Quaternion.FromToRotation(transform.forward, groundAngle);

				// lock foot to position
				if (!rightFootLocked)
				{
					rightFootLocked = true;
					rightFootLockPos = ikFootPos;
					rightFootLockRot = footPitchRotation;
				}

				// move ik pos
				animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(rightFootLockPos.x, avgHitY + legDistance, rightFootLockPos.z));

				// rotate to position
				animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootLockRot * transform.rotation);
			}
			
			///=======

			heightTarget = ccHeightGrounded - Mathf.Round(Mathf.Abs(heelHitL.point.y - heelHitR.point.y) * 0.75f * 5) / 5; // reduce jitter with rounded target
		}
	}
}
