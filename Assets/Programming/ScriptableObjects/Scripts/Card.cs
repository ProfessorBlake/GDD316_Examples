using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image portraitImage;

        private CardData cardData;

        public void Init(CardData newCardData)
        {
            cardData = newCardData;
            nameText.text = cardData.Name;
            descriptionText.text = cardData.Description;
            portraitImage.sprite = cardData.Sprite;
        }
    }
}
