using UnityEngine;

namespace Game.Programming
{
    [CreateAssetMenu(fileName = "Checkpoint", menuName ="Achievements/Criteria/Checkpoint")]
    public class CheckpointCriteria : AchievementCriteria
    {
        [SerializeField] private int checkpointId;
		private AchievementData parentAchievement;

		public override void Init(AchievementData parent)
		{
			parentAchievement = parent;
			criteriaMet = false;
			//Debug.Log("Init Checkpoint Achivement: " +  checkpointId);
			Ball.Instance.OnTriggerCheckpoint += HandleCheckpoint;
		}

		public override void Clear()
		{
			//Debug.Log("Clear Checkpoint Achivement: " +  checkpointId);
			Ball.Instance.OnTriggerCheckpoint -= HandleCheckpoint;
		}

		private void HandleCheckpoint(int id)
		{
			if(checkpointId == id)
			{ 
				criteriaMet = true;
				Clear();
				parentAchievement.CheckCriteria();
			}
		}
	}
}
