using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.AnimationIK
{
    public class ItemDetection : MonoBehaviour
    {
		public UnityEvent<RaycastHit> OnLeftHandItemFound;
		public UnityEvent<RaycastHit> OnRightHandItemFound;


		[SerializeField] private Transform eyes;
		[SerializeField] private float itemCheckDistance;
		[SerializeField] private float handOffset;
		[SerializeField] private LayerMask checkLayers;

		private void Update()
		{
			for (int i = 0; i < 20; i++)
			{
				//left hand
				if (Physics.SphereCast(eyes.position, 0.25f,
					(eyes.forward + new Vector3(0,-i * 0.05f, 0f)) +
					(eyes.right * handOffset), 
					out RaycastHit hitl, itemCheckDistance, checkLayers.value))
				{
					OnLeftHandItemFound?.Invoke(hitl);
					break;
				}
			}
			for (int i = 0; i < 20; i++)
			{
				//right hand
				if (Physics.SphereCast(eyes.position, 0.25f,
					(eyes.forward + new Vector3(0, -i * 0.05f, 0f)) +
					(eyes.right * -handOffset), 
					out RaycastHit hitr, itemCheckDistance, checkLayers.value))
				{
					OnRightHandItemFound?.Invoke(hitr);
					break;
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			//if (eyes == null) return;
			//Gizmos.color = Color.green;
			//for (int i = 0; i < 30; i++)
			//{
			//	Gizmos.DrawLine(eyes.position, eyes.position + 
			//		((eyes.forward + new Vector3(0, -i*0.05f, 0)) * itemCheckDistance) +
			//		(eyes.right * handOffset));
			//	Gizmos.DrawLine(eyes.position, eyes.position +
			//		((eyes.forward + new Vector3(0, -i * 0.05f, 0)) * itemCheckDistance) +
			//		(eyes.right * -handOffset));
			//}
		}
	}
}
