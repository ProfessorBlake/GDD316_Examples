using TMPro;
using UnityEngine;

namespace Game
{
    public class BoxInspection : MonoBehaviour
    {
        [SerializeField] private TMP_Text txt;
		[SerializeField] private float holdTime;

		private float touchTime;
		private Yellowbox box;
		private int score;

		private void Start()
		{
			txt.text = score.ToString().PadLeft(2, '0');
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (box == null && collider.transform.TryGetComponent<Yellowbox>(out Yellowbox hitbox))
			{
				box = hitbox;
				touchTime = Time.time;
			}
		}

		private void OnTriggerStay(Collider collider)
		{
			if (collider.transform.TryGetComponent<Yellowbox>(out Yellowbox hitbox))
			{
				if (box != null && hitbox == box)
				{
					if (Time.time >= touchTime + holdTime)
					{
						score++;
						txt.text = score.ToString().PadLeft(2, '0');
						box = null;
					}
				}
			}
		}

		private void OnTriggerExit(Collider collider)
		{
			if (collider.transform.TryGetComponent<Yellowbox>(out Yellowbox hitbox))
			{
				if(box != null && hitbox == box)
				{
					box = null;
				}
			}
		}
	}
}
