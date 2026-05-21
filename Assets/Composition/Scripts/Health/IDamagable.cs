using UnityEngine;

namespace Game.Composition
{
    public interface IDamagable
    {
        public void TakeDamage(float damage, SpellEffectData data);        
    }
}
