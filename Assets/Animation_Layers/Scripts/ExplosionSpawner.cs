using UnityEngine;

namespace Game.AnimationLayers
{
    public class ExplosionSpawner : MonoBehaviour
    {
		[SerializeField] private GameObject explosionPrefab;
        [SerializeField] private Vector2 spawnRange;
		[SerializeField] private Player player;

		private float boomPosition;

		private void Start()
		{
			SetNextBoom();
		}

		private void SetNextBoom()
		{
			boomPosition = transform.position.z + Random.Range(spawnRange.x, spawnRange.y);
		}

		private void Update()
		{
			if (transform.position.z >= boomPosition)
			{
				GameObject boom = Instantiate(
					explosionPrefab, 
					transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), 
					Quaternion.identity);
				Destroy(boom, 3f);
				player.TriggerInjury();
				SetNextBoom();
			}
		}
	}
}
