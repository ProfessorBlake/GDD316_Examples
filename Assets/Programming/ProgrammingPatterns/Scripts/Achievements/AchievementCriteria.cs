using UnityEngine;

namespace Game.Programming
{
    public abstract class AchievementCriteria : ScriptableObject
    {
        public bool CriteriaMet => criteriaMet;
        protected bool criteriaMet;

        public abstract void Init(AchievementData parent);     // Subscribe to trackers, store parent ref to alert later
        public abstract void Clear();    // Clear subscribers
    }
}
