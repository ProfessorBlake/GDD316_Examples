using UnityEngine;

namespace Game.Composition
{
	/// <summary>
	/// Move a spell in direction of velocity
	/// </summary>
	public class MoveStraight : MovementEffect
	{
		[SerializeField] private float speed;

		public override void Init(SpellEffectData newData)
		{
			data = newData;
		}

		public override void Apply(SpellEffectData data)
		{
			transform.position += transform.up * speed * Time.deltaTime;
		}
	}
}
