using System;
using UnityEngine;

namespace Game.Programming
{
    [CreateAssetMenu(fileName ="Distance", menuName ="Achievements/Criteria/Distance")]
    public class DistanceCriteria : AchievementCriteria
    {
        [SerializeField] private float targetDistance;
		private AchievementData parentAchievement;

		public override void Init(AchievementData parent)
		{
			parentAchievement = parent;
			criteriaMet = false;
			//Debug.Log("Init Distance Achivement: " +  targetDistance);
            DistanceTracker.OnDistanceUpdate += HandleDistanceUpdate;
		}

		public override void Clear()
		{
			//Debug.Log("Clear Distance Achivement: " + targetDistance);
			DistanceTracker.OnDistanceUpdate -= HandleDistanceUpdate;
		}

		private void HandleDistanceUpdate(float dist)
		{
			if(dist >= targetDistance)
			{ 
				criteriaMet = true;
				Clear();
				parentAchievement.CheckCriteria();
			}
		}
	}
}
