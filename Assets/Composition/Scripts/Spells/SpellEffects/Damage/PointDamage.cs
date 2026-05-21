using UnityEngine;

namespace Game.Composition
{
    public class PointDamage : MonoBehaviour, ISpellEffect
    {
        [SerializeField] private float damage;

		public SpellEffectData OnSpellHit(SpellEffectData data)
		{
			if(data.Target.TryGetComponent<IDamagable>(out IDamagable hit))
			{
				hit.TakeDamage(damage, data);
			}

			return new SpellEffectData()
			{
				PreserveSpell = false
			};
		}
    }
}
