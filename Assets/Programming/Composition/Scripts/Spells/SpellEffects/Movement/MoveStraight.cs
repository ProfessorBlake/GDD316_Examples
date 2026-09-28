using UnityEngine;

namespace Game.Composition
{
	/// <summary>
	/// Move a spell in direction of velocity
	/// </summary>
	public class MoveStraight : MonoBehaviour, ISpellEffect
	{
		[SerializeField] private float speed;

		public void Init(SpellEffectData newData)
		{

		}

		public void Apply(SpellEffectData data)
		{
			transform.position += transform.up * speed * Time.deltaTime;
		}

		public SpellEffectData OnSpellHit(SpellEffectData data)
		{
			return SpellEffectData.Empty();
		}
	}
}
