using Game.Programming;
using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class ResetOnFall : MonoBehaviour
    {
        private Vector3 startPos;
        private Quaternion startRot;
		
		private void Start()
		{
			Ball.Instance.OnReset += HandleBallFall;

			startPos = transform.position;
			startRot = transform.rotation;
		}

		private void OnDestroy()
		{
			Ball.Instance.OnReset -= HandleBallFall;
		}

		private void HandleBallFall()
		{
			transform.position = startPos;
			transform.rotation = startRot;
			if(TryGetComponent<Rigidbody>(out Rigidbody rb))
			{
				rb.linearVelocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				rb.position = startPos;
				rb.rotation = startRot;
			}

		}
	}
}
