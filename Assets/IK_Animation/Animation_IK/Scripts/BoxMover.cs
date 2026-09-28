using UnityEngine;

namespace Game
{
    public class BoxMover : MonoBehaviour
    {
		[SerializeField] private Vector3 force;
		[SerializeField] private MeshRenderer beltMesh;

		private Vector2 textOffset;
		private Material beltMat;

		private void Start()
		{
			beltMat = beltMesh.material;
		}

		private void Update()
		{
			textOffset += new Vector2(0, force.z * Time.deltaTime);
			beltMat.mainTextureOffset = textOffset;
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.attachedRigidbody.isKinematic) return;
			Vector3 dif = force - other.attachedRigidbody.linearVelocity;
			if (dif.sqrMagnitude < force.sqrMagnitude)
			{
				other.attachedRigidbody.AddForceAtPosition(force * dif.sqrMagnitude, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), ForceMode.Force);
			}
		}
	}
}
