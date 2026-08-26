using UnityEngine;

namespace Game
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform target;

		private void OnTriggerEnter(Collider other)
		{
			other.attachedRigidbody.MovePosition(target.position);
			other.attachedRigidbody.linearVelocity = Vector3.zero;
			other.attachedRigidbody.angularVelocity = Vector3.zero;
		}

		private void OnDrawGizmosSelected()
		{
			if (target != null)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(target.position, 0.1f);
			}
		}
	}
}
