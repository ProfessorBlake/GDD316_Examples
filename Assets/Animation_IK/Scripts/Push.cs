using UnityEngine;

namespace Game
{
    public class Push : MonoBehaviour
    {
		[SerializeField] private Vector3 moveTarget;
		[SerializeField] private float moveSpeed;

		private Vector3 start;

		private void Start()
		{
			start = transform.position;
		}

		private void FixedUpdate()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				transform.position = Vector3.Lerp(
					transform.position,
					moveTarget,
					moveSpeed * Time.fixedDeltaTime
					);
			}
			else
			{
				transform.position = Vector3.Lerp(
					transform.position,
					start,
					moveSpeed * Time.fixedDeltaTime
					);
			}
		}
	}
}
