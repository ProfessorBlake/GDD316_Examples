using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game.Composition
{
    public class Spell : MonoBehaviour
    {
		[SerializeField] private float radius;

		private SpellEffect[] spellEffects;
		private GameObject owner;

		private void OnEnable()
		{
			spellEffects = GetComponents<SpellEffect>();
		}

		private void Start()
		{
			foreach (SpellEffect effect in spellEffects)
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

			foreach (SpellEffect effect in spellEffects)
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
				bool preserve = false;
				foreach(SpellEffect effect in spellEffects)
				{
					SpellEffectData returnedData = effect.OnSpellHit(new SpellEffectData()
					{
						SpellInstance = this.gameObject,
						Target = hit.gameObject,
						Caster = owner
					});

					if (returnedData.PreserveSpell)
					{
						preserve = true;
					}
				}

				if (!preserve)
				{
					Destroy(gameObject);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
