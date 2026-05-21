using UnityEngine;

namespace Game.Composition
{
    public class PointDamage : DamageEffect
    {
        [SerializeField] private float damage;

		public override SpellEffectData OnSpellHit(SpellEffectData data)
		{
			if(data.Target.TryGetComponent<Health>(out Health hit))
			{
				//hit.
			}

			return new SpellEffectData()
			{
				PreserveSpell = false
			};
		}
    }
}
