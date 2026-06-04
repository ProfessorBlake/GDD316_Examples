using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.AnimationLayers
{
    public class InfoUpdater : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Slider weightSlider;
        [SerializeField] private Slider tiredSlider;
		[SerializeField] private Slider injurySlider;

        private Player player;

		private void Awake()
		{
            player = FindAnyObjectByType<Player>();
            if (player == null)
            {
                Debug.LogError("InfoUpdater:: Unable to find player!");
                this.enabled = false;
            }
		}

		private void OnEnable()
		{
            if (player != null)
            {
                player.OnMoveSpeedUpdate += HandleMovespeedUpdate;
				player.OnTiredUpdate += HandleTiredUpdate;
				player.OnCarryUpdate += HandleCarryUpdate;
				player.OnInjuryUpdate += HandleInjuryUpdate;
			}
		}

		private void OnDisable()
		{
			if (player != null)
			{
				player.OnMoveSpeedUpdate -= HandleMovespeedUpdate;
				player.OnTiredUpdate -= HandleTiredUpdate;
				player.OnCarryUpdate -= HandleCarryUpdate;
				player.OnInjuryUpdate -= HandleInjuryUpdate;
			}
		}

		private void HandleCarryUpdate(float value)
		{
			weightSlider.value = value;
			weightSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.cyan, Color.orange, value);
		}

		private void HandleTiredUpdate(float value)
		{
			tiredSlider.value = value;
			tiredSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.cyan, Color.orange, value);
		}

		private void HandleMovespeedUpdate(float value)
		{
			speedSlider.value = value;
			speedSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.cyan, Color.orange, value);
		}

		private void HandleInjuryUpdate(float value)
		{
			injurySlider.value = value;
			injurySlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, value);
		}
	}
}
