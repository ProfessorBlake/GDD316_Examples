using System;
using UnityEngine;

namespace Game.Programming
{
	/// <summary>
	/// Tracks distance of ball
	/// </summary>
    public class DistanceTracker : MonoBehaviour
    {
		public static Action<float> OnDistanceUpdate;
		
		private float totalDistance = -1f;
		private Vector3 lastPosition;

		private void Start()
		{
			Ball.Instance.OnMoved += HandleBallMoved;
		}

		private void OnDisable()
		{
			Ball.Instance.OnMoved -= HandleBallMoved;
		}

		private void HandleBallMoved(Vector3 position)
		{
			if(totalDistance < 0) // Handle initial position
			{
				lastPosition = position;
				totalDistance = 0f;
			}
			else
			{
				totalDistance += Vector3.Distance(lastPosition, position);
				lastPosition = position;
			}
			OnDistanceUpdate?.Invoke(totalDistance);
		}
	}
}
