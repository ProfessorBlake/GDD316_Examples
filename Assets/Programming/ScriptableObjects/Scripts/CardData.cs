using System;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
	public class CardData : ScriptableObject
	{
		public Action OnHealthChanged;

		public string Name => name;
		public string Description => description;
		public Sprite Sprite => sprite;
		public int Health => health;
		public float Speed => speed;

		[SerializeField] private string name;
		[SerializeField] private string description;
		[SerializeField] private Sprite sprite;
		[SerializeField][Range(1, 25)] private int health;
		[SerializeField][Range(0f, 1f)] private float speed;
	}
}
