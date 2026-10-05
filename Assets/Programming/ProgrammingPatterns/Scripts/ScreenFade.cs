using UnityEngine;
using UnityEngine.UI;

namespace Game.Programming
{
    public class ScreenFade : MonoBehaviour
	{
		[Header("Effects")]
		[SerializeField] private Image flashImage;

		private void Awake()
		{
			FindAnyObjectByType<Ball>().OnReset += HandleReset;
		}

		private void OnDisable()
		{
			FindAnyObjectByType<Ball>().OnReset -= HandleReset;
		}

		private void Update()
		{
			flashImage.color = new Color(0, 0, 0, Mathf.MoveTowards(flashImage.color.a, 0f, 0.75f * Time.deltaTime));
		}

		private void HandleReset()
		{
			flashImage.color = Color.black;
		}
	}
}
