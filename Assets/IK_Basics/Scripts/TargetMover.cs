using UnityEngine;

namespace Game
{
    public class TargetMover : MonoBehaviour
    {
        [SerializeField] private Vector2 range;
        [SerializeField] private float speed;
        [SerializeField] private Transform circle;

		private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(input != Vector3.zero)
			{
                transform.forward = input.normalized;
				Vector2 movePos = transform.position + (transform.forward * speed * Time.deltaTime);
				if (Mathf.Abs(movePos.x) > range.x || Mathf.Abs(movePos.y) > range.y)
				{
					input *= -1f;
					return;
				}
				transform.position = movePos;
				circle.position = transform.position;
			}
            
		}
    }
}
