using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Programming
{
    public class AchievementManager : MonoBehaviour
    {
		public static AchievementManager Instance => instance;

		private static AchievementManager instance;

		[SerializeField] private AchievementData[] achievements;
		[Header("Popup")]
		[SerializeField] private GameObject popup;
		[SerializeField] private TMP_Text popupTitle;
		[SerializeField] private TMP_Text popupDescription;
		[SerializeField] private Image popupPortrait;
		[SerializeField] private AudioClip popupSnd;

		private List<AchievementData> showAchivementQueue = new List<AchievementData>();
		private Coroutine showAchivementsCoroutine;

		private void Awake()
		{
			if(instance != null)
			{
				DestroyImmediate(this.gameObject);
				return;
			}
			instance = this;
			popup.SetActive(false);
		}

		private void Start()
		{
			for (int i = 0; i < achievements.Length; i++)
			{
				achievements[i].Init();
			}
		}

		private void OnDisable()
		{
			for (int i = 0; i < achievements.Length; i++)
			{
				achievements[i].Clear();
			}
		}

		public void ShowAchivement(AchievementData data)
		{
			showAchivementQueue.Add(data);
			if(showAchivementsCoroutine == null)
			{
				showAchivementsCoroutine = StartCoroutine(ShowAchivementPopup());
			}
		}

		/// <summary>
		/// Show each achievement in queue for some time
		/// </summary>
		/// <returns></returns>
		private IEnumerator ShowAchivementPopup()
		{
			while(showAchivementQueue.Count > 0)
			{
				AudioSource.PlayClipAtPoint(popupSnd, Ball.Instance.transform.position);
				popup.SetActive(true);
				popupTitle.text = showAchivementQueue[0].Title;
				popupDescription.text = showAchivementQueue[0].Description;
				popupPortrait.sprite = showAchivementQueue[0].Image;
				yield return new WaitForSecondsRealtime(5f);
				popup.SetActive(false);
				showAchivementQueue.RemoveAt(0);
				yield return new WaitForSecondsRealtime(1f);
			}

			showAchivementsCoroutine = null;
		}
	}
}
