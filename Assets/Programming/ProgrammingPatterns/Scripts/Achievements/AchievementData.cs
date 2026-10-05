using UnityEngine;

namespace Game.Programming
{
    [CreateAssetMenu(fileName = "Achievement Data", menuName = "Achievements/Achievement")]
    public class AchievementData : ScriptableObject
    {
        public string Title => title;
        public string Description => description;
        public bool Met => met;
        public Sprite Image => image;

        [SerializeField] private AchievementCriteria[] criteria;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite image;
        private bool met;

        /// <summary>
        /// Have criteria subscribe to relevant trackers
        /// </summary>
        public void Init()
        {
            met = false;
            for (int i = 0; i < criteria.Length; i++)
            {
                criteria[i].Init(this);
            }
        }

        public void Clear()
		{
			for (int i = 0; i < criteria.Length; i++)
			{
				criteria[i].Clear();
			}
		}

        /// <summary>
        /// Criteria can tell parent data to check for total progress
        /// </summary>
        /// <returns></returns>
        public void CheckCriteria()
        {
            if (met) return;

            string s = "Criteria check for " + this.name;
			for (int i = 0; i < criteria.Length; i++)
			{
                if (!criteria[i].CriteriaMet)
				{
                    s += ": " + criteria[i].name + " not met";
                    Debug.Log(s);
					return;
				}
			}
            s += " : All met!";
            Debug.Log(s);
            met = true;
            AchievementManager.Instance.ShowAchivement(this);
            Clear();
		}
    }
}
