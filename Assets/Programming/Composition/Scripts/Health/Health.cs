using UnityEngine;

namespace Game.Composition
{
    public class Health : MonoBehaviour, IDamagable
    {
		[SerializeField] private float maxHealth;

		[SerializeField] private float health;

		private void Awake()
		{
			health = maxHealth;
		}

		public void TakeDamage(float damage, SpellEffectData data)
		{
			health -= damage;
		}
    }
}
