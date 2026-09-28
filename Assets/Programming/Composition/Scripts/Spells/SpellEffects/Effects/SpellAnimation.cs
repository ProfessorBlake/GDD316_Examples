using UnityEngine;

namespace Game.Composition
{
	/// <summary>
	/// Spawn an animation when triggered
	/// </summary>
	public class SpellAnimation : MonoBehaviour, ISpellEffect
	{
		[SerializeField] private GameObject animatedSpritePrefab;

		public void Init(SpellEffectData data)
		{
			GameObject ani = Instantiate(animatedSpritePrefab, data.SpellInstance.transform);
		}

		public void Apply(SpellEffectData data)
		{

		}

		public SpellEffectData OnSpellHit(SpellEffectData data)
		{
			return SpellEffectData.Empty();
		}
	}
}
