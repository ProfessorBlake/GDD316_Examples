using UnityEngine;

namespace Game.Composition
{
    public class PointDamage : MonoBehaviour, ISpellEffect
    {
        [SerializeField] private float damage;

		public void Apply(SpellEffectData data)
		{

		}

		public void Init(SpellEffectData data)
		{

		}

		public SpellEffectData OnSpellHit(SpellEffectData data)
		{
			if(data.Target.TryGetComponent<IDamagable>(out IDamagable hit))
			{
				hit.TakeDamage(damage, data);
			}

			return SpellEffectData.Empty();
		}
    }
}
