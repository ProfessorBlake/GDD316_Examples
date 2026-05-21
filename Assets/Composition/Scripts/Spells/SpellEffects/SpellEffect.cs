using UnityEngine;

namespace Game.Composition
{
	public abstract class SpellEffect : MonoBehaviour
	{
		public ESpellTypes SpellType => spellType;
		[SerializeField] protected ESpellTypes spellType;

		protected SpellEffectData data;

		public virtual void Init(SpellEffectData data){ }
		public virtual void Apply(SpellEffectData data){}
		/// <summary>
		/// Informs SpellEffects of a collision.
		/// </summary>
		/// <param name="data"></param>
		/// <returns>Returns data informing Spell if it should be destroyed.</returns>
		public virtual SpellEffectData OnSpellHit(SpellEffectData data) 
		{
			return new SpellEffectData()
			{
				PreserveSpell = false
			};
		}
	}
}