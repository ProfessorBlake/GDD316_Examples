using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game.Composition
{
    public class Spell : MonoBehaviour
    {
		public Action<SpellEffectData> OnSpellHit;

		[SerializeField] private float radius;

		private ISpellEffect[] spellEffects;
		private GameObject owner;

		private void OnEnable()
		{
			spellEffects = GetComponents<ISpellEffect>();
		}

		private void Start()
		{
			foreach (ISpellEffect effect in spellEffects)
			{
				effect.Init(new SpellEffectData()
				{
					SpellInstance = this.gameObject
				});
			}
		}

		private void Update()
		{
			HitDetect();

			foreach (ISpellEffect effect in spellEffects)
			{
				effect.Apply(new SpellEffectData()
				{
					SpellInstance = this.gameObject
				});
			}
		}

		private void HitDetect()
		{
			Collider2D hit = Physics2D.OverlapCircle(transform.position, radius);
			if (hit != null)
			{
				OnSpellHit?.Invoke(new SpellEffectData() { SpellInstance = gameObject }); 
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
