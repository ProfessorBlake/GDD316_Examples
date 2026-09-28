using UnityEngine;

namespace Game.Composition
{
	public interface ISpellEffect
	{
		public void Init(SpellEffectData data);
		public void Apply(SpellEffectData data);
		/// <summary>
		/// Informs SpellEffects of a collision.
		/// </summary>
		/// <param name="data"></param>
		/// <returns>Returns data informing Spell if it should be destroyed.</returns>
		public SpellEffectData OnSpellHit(SpellEffectData data);
	}
}