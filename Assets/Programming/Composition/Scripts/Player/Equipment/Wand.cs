using UnityEngine;

namespace Game.Composition
{
    public class Wand : MonoBehaviour, IPlayerUsable
    {
		[SerializeField] private GameObject spellPrefab;
		[SerializeField] private float fireDelay;

		private float delay;
		[SerializeField] private bool use;

		private void Update()
		{
			delay -= Time.deltaTime;
			if (delay < 0)
			{
				if (use)
				{
					delay = fireDelay;
					GameObject spell = Instantiate(spellPrefab, transform.position, Quaternion.identity);
					spell.transform.up = transform.up;
				}
			}
		}

		public void BeginUse()
		{
			if(!use)
			{
				use = true;
			}
			
		}

		public void EndUse()
		{
			if (use)
			{
				use = false;
			}
		}
    }
}
