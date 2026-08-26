using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace Game.IK
{
	[RequireComponent(typeof(LineRenderer))]
	public class IK_Demo : MonoBehaviour
	{
		[SerializeField] private int chainCount = 3;
		[SerializeField] private float segmentLength = 1f;
		[SerializeField] private Transform targetTransform;
		[SerializeField] private int solverIterations = 5;
		[SerializeField] private ChainSegment[] chains = new ChainSegment[0];
		[SerializeField] private float targetTolerance = 0.1f;

		private float totalLength;
		private LineRenderer lineRenderer;

		[System.Serializable]
		public class ChainSegment
		{
			public Vector2 Origin;
			public float Length;
		}

		private void Awake()
		{
			lineRenderer = GetComponent<LineRenderer>();
		}

		private void Start()
		{
			SetupChain();
		}

		private void SetupChain()
		{
			Debug.Log("Building chain");

			chains = new ChainSegment[chainCount];			// init array
			for(int i = 0; i < chains.Length; i++)
			{
				chains[i] = new ChainSegment()					// create new chain segment
				{
					Origin = transform.position + transform.right * (i * segmentLength),	// set origin
					Length = i < chainCount ? segmentLength : 0								// set lengths, last one = 0
				};
				totalLength += chains[i].Length;
			}
			
			lineRenderer.positionCount = chains.Length;			// set lr positions count
		}

		private void Update()
		{
			UpdateIKSolution();

			Vector3[] positions = new Vector3[chains.Length];
			for (int i = 0; i < chains.Length; i++)
			{
				positions[i] = chains[i].Origin;
			}
			lineRenderer.SetPositions(positions);
		}

		private void UpdateIKSolution()
		{
			float distanceToTarget = Vector3.Distance(chains[0].Origin, targetTransform.position);

			// point straight at unreachable target
			if (distanceToTarget >= totalLength)
			{
				Vector2 direction = ((Vector2)targetTransform.position - chains[0].Origin).normalized;
				for (int i = 1; i < chains.Length; i++)
				{
					chains[i].Origin = chains[i - 1].Origin + direction * chains[i - 1].Length;
				}
				return; // skip FABRIK steps since we can't reach target
			}

			for (int i = 0; i < solverIterations; i++)
			{
				// close enough to target
				if (Vector3.Distance(chains[chains.Length - 1].Origin, targetTransform.position) < targetTolerance)
					break;

				// backward 
				chains[chains.Length - 1].Origin = targetTransform.position;	// set last chain to target pos
				for (int j = chains.Length - 2; j >= 0; j--)
				{
					Vector2 dir = (chains[j].Origin - chains[j + 1].Origin).normalized;
					chains[j].Origin = chains[j + 1].Origin + dir * chains[j].Length;
				}

				// forward
				chains[0].Origin = transform.position; // Keep root anchored
				for (int k = 1; k < chains.Length; k++)
				{
					Vector2 dir = (chains[k].Origin - chains[k - 1].Origin).normalized;
					chains[k].Origin = chains[k - 1].Origin + dir * chains[k - 1].Length;
				}
			}
		}

		private void OnDrawGizmos()
		{
			if (targetTransform != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(targetTransform.position, 0.4f + Mathf.Sin(Time.time * 3f) * 0.15f);
			}

			if (chains.Length <= 0) return;

			Gizmos.color = Color.green;
			for (int i = 0; i < chains.Length; i++)
			{
				Gizmos.DrawWireSphere(chains[i].Origin, 0.1f);
				if(i < chains.Length - 1)
					Gizmos.DrawLine(chains[i].Origin, chains[i+1].Origin);
			}
		}
	}
}
