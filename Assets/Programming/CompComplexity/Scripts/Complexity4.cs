using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Programming
{
	/// <summary>
	/// This script executes a O(n^2) complexity function. The time to complete
	/// scales with the square of n
	/// </summary>
    public class Complexity4 : MonoBehaviour
    {
		[SerializeField] private int size;
		[SerializeField] private TMP_Text timeText;
		private DateTime[] birthdays;

		private void Start()
		{
			birthdays = new DateTime[size];
			for (int i = 0; i < birthdays.Length; i++)
			{
				DateTime dt = new DateTime(2026, 1, 1);
				dt = dt.AddDays(Random.Range(0, 365));
				birthdays[i] = dt;
			}
		}

		public void Begin()
		{
			// Create array of size
			birthdays = new DateTime[size];
			for (int i = 0; i < birthdays.Length; i++)
			{
				DateTime dt = new DateTime(2026, 1, 1);
				dt = dt.AddDays(Random.Range(0, 365));
				birthdays[i] = dt;
			}

			float t = Time.realtimeSinceStartup;
			FindDuplicates();
			t = (Time.realtimeSinceStartup - t);
			timeText.text = t + " s";
		}

		/// <summary>
		/// Every item in the array checks to see how many time it appears in the array
		private void FindDuplicates()
		{
			for (int i = 0; i < birthdays.Length; i++) // Each person
			{
				int duplicates = 0;
				for (int j = 0; j < birthdays.Length; j++) // Askes each other person
				{
					if (i == j) continue; // spiderman_pointing.bmp
					if (birthdays[i].CompareTo(birthdays[j]) == 0) duplicates++; // Found someone with a matching bday
				}
				if(duplicates > 0) 
					Debug.Log($"Person {i + 1}({birthdays[i].ToShortDateString()}) found {duplicates} matches!");
			}
		}
	}
}
