using System;
using UnityEngine;

namespace Game.Programming
{
    [CreateAssetMenu(fileName ="Time", menuName ="Achievements/Criteria/Time")]
    public class TimeCriteria : AchievementCriteria
    {
		public enum TimeTypeEnum { LessThanTarget, GreaterThanTarget}

        [SerializeField] private float targetTime;
		[SerializeField] private TimeTypeEnum timeType;

		private AchievementData parentAchievement;

		public override void Init(AchievementData parent)
		{
			parentAchievement = parent;
			//Debug.Log("Init Distance Achivement: " + targetTime);
            TimeTracker.OnTimeUpdate += HandleTimeUpdate;
		}

		public override void Clear()
		{
			//Debug.Log("Clear Distance Achivement: " + targetTime);
			TimeTracker.OnTimeUpdate -= HandleTimeUpdate;
		}

		private void HandleTimeUpdate(float t)
		{
			// Met while time < target
			if(timeType == TimeTypeEnum.LessThanTarget)
			{
				criteriaMet = t < targetTime;
			}
			else if (timeType == TimeTypeEnum.GreaterThanTarget)
			{
				criteriaMet = t > targetTime;
			}
		}
	}
}
