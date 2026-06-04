using UnityEngine;

namespace Game.Composition
{
	/// <summary>
	/// Move a spell in direction of velocity
	/// </summary>
	public class MoveWave : MonoBehaviour, ISpellEffect
	{
		[SerializeField] private float speed;
		[SerializeField] private float amplitude;
		[SerializeField] private float frequency;

		private Vector3 startPos;
		private float dist;
		private float t;

		public void Init(SpellEffectData newData)
		{
			startPos = newData.SpellInstance.transform.position;
			transform.up = newData.SpellInstance.transform.up;
			dist = 0;
		}

		private void Update()
		{
			dist += speed * Time.deltaTime;
			t += Time.deltaTime;
			transform.position = startPos + (transform.up * dist) + (transform.right * Mathf.Sin(t * frequency) * amplitude);
		}

		public void Apply(SpellEffectData data)
		{
			
		}

		public SpellEffectData OnSpellHit(SpellEffectData data)
		{
			return SpellEffectData.Empty();
		}
	}
}
