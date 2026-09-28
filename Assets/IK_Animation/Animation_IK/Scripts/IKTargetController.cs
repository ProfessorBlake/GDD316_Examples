using UnityEngine;

namespace Game.AnimationIK
{
    public class IKTargetController : MonoBehaviour
    {
        [SerializeField] private Transform ikTargetTransform;
        [SerializeField] private Vector3 targetPosition;
		[SerializeField] private Vector3 targetDirection;
        [SerializeField] private float moveSpeed = 5f;
		[SerializeField] private float surfaceOffset = 0.01f;

		[Header("Collider")]
		[SerializeField] private Rigidbody ikCollider;
		[SerializeField] private Transform ikColliderTarget;

		private Vector3 restPosition;
		private int resetTime;

		private void OnValidate()
		{
			if (ikCollider != null && ikColliderTarget != null)
			{
				ikCollider.transform.position = ikColliderTarget.position;
			}
		}

		private void Awake()
		{
			if (ikTargetTransform != null)
			{
				restPosition = ikTargetTransform.localPosition;
				targetPosition = ikTargetTransform.position;
			}
		}

		private void Update()
		{
			resetTime--;
			if (resetTime < 0)
				targetPosition = restPosition;

			ikTargetTransform.localPosition = Vector3.Lerp(ikTargetTransform.localPosition, targetPosition, moveSpeed *  Time.deltaTime);
			//Quaternion targROt = Quaternion.FromToRotation( -Vector3.up, targetDirection);
			Quaternion targROt = Quaternion.LookRotation(-targetDirection, Vector3.up);
			ikTargetTransform.rotation = Quaternion.Slerp(ikTargetTransform.rotation, targROt, 15 * Time.deltaTime);
		}

		private void FixedUpdate()
		{
			ikCollider.MovePosition(ikColliderTarget.position);
		}

		public void SetTarget(RaycastHit hit)
        {
			targetPosition = transform.InverseTransformPoint(hit.point + hit.normal * surfaceOffset);
			targetDirection = hit.normal;
			resetTime = 5;
        }
    }
}
