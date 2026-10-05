using System.Collections;
using UnityEngine;

namespace Game.Programming
{
	public class CardManager : MonoBehaviour
    {
		[SerializeField] private CardData[] cardData;
		[SerializeField] private Transform cardContainer;
		[SerializeField] private Card cardPrefab;

		[SerializeField] private Vector3 moveCenter;
		[SerializeField] private float spread;
		[SerializeField] private float moveSpeed;

		private Card[] cards;

		private void Start()
		{
			cards = new Card[cardData.Length];
			for(int i = 0; i < cards.Length; i++)
			{
				CardData copy = cardData[i];
				Card newCard = Instantiate(cardPrefab, cardContainer);
				newCard.Init(cardData[i]);
				cards[i] = newCard;
				RectTransform rect = newCard.GetComponent<RectTransform>();
				rect.position = new Vector3(Screen.width / 2 + moveCenter.x, Screen.height / 2 + moveCenter.y);
			}

			StartCoroutine(IntroAni());
		}

		private void Update()
		{
			for (int i = 0; i < cards.Length; i++)
			{
				RectTransform rect = cards[i].GetComponent<RectTransform>();

				Vector3 targ = new Vector3(Screen.width/2+ moveCenter.x, Screen.height/2 + moveCenter.y) +
					new Vector3((-spread * cards.Length-1)/2 + (i * spread), 0, 0);
				rect.position = Vector3.Lerp(rect.position, targ,moveSpeed * Time.deltaTime);
			}
		}

		private IEnumerator IntroAni()
		{
			moveSpeed = 1f;
			spread = 500f;
			moveCenter = new Vector3(0, 100, 0);
			yield return new WaitForSeconds(1.5f);

			moveSpeed = 2f;
			moveCenter = new Vector3(0, 0, 0);
			spread = 250f;
			yield return new WaitForSeconds(2f);

			moveSpeed = 10f;
			moveCenter = new Vector3(0, -300, 0);
			spread = 0f;

		}
	}
}
