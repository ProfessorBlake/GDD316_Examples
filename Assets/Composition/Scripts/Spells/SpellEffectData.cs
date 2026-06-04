using UnityEngine;

namespace Game.Composition
{
    public struct SpellEffectData
    {
        public GameObject SpellInstance;    // GameObject spell component is attached to
        public GameObject Caster;           // Who/what casted the spell
        public GameObject Target;           // Intended target / hit object
        public Vector3 TargetPosition;
        public bool PreserveSpell;          // Prevent spell from beign destroyed

		public static SpellEffectData Empty()
		{
			return new SpellEffectData()
			{
				PreserveSpell = false
			};
		}
	}
}