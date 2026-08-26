using UnityEngine;

namespace Game.IK
{
	[RequireComponent(typeof(LineRenderer))]
	public class IK_Demo_Roam : MonoBehaviour
	{
		[SerializeField] private int chainCount = 3;
		[SerializeField] private float segmentLength = 1f;
		[SerializeField] private Transform targetTransform;
		[SerializeField] private int solverIterations = 5;
		[SerializeField] private ChainSegment[] chains = new ChainSegment[0];
		[SerializeField] private float targetTolerance = 0.1f;
		[SerializeField] private float waveAmplitude = 1f;
		[SerializeField] private float waveFrequency = 1f;

		private float totalLength;
		private LineRenderer lineRenderer;
		Vector3[] positions;

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
			positions = new Vector3[chains.Length];
		}

		private void SetupChain()
		{
			chains = new ChainSegment[chainCount];          // init array
			for (int i = 0; i < chains.Length; i++)
			{
				chains[i] = new ChainSegment()                  // create new chain segment
				{
					Origin = transform.position + transform.right * (i * segmentLength),    // set origin
					Length = i < chainCount ? segmentLength : 0                             // set lengths, last one = 0
				};
				totalLength += chains[i].Length;
			}

			lineRenderer.positionCount = chains.Length;         // set lr positions count
		}

		private void Update()
		{
			UpdateIKSolution();

			for (int i = 0; i < chains.Length; i++)
			{
				positions[i] = chains[i].Origin;
			}
			lineRenderer.SetPositions(positions);
		}

		private void UpdateIKSolution()
		{
			for (int i = 0; i < solverIterations; i++)
			{
				// backward 
				chains[chains.Length - 1].Origin = targetTransform.position;    // set last chain to target pos
				for (int j = chains.Length - 2; j >= 0; j--)
				{
					Vector2 dir = (chains[j].Origin - chains[j + 1].Origin).normalized;
					chains[j].Origin = chains[j + 1].Origin + dir * chains[j].Length;
				}
			}

			// apply wave offset
			for (int i = 1; i < chains.Length; i++)
			{
				Vector2 dir = (chains[0].Origin - (Vector2)targetTransform.position).normalized;
				chains[i].Origin += new Vector2(-dir.y, dir.x).normalized * (Mathf.Sin(Time.time + i * waveFrequency)) * Mathf.Pow(chains.Length-i,waveAmplitude) * 0.001f;
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
				if (i < chains.Length - 1)
					Gizmos.DrawLine(chains[i].Origin, chains[i + 1].Origin);
			}
		}
	}
}
