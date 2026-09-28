using TMPro;
using UnityEngine;

namespace Game.Programming
{
	/// <summary>
	/// This script executes a O(log n) complexity function. The time to complete
	/// this function scales with items by log(n)
	/// Attempts to find the index of a score in a leaderboard
	/// </summary>
    public class Complexity2 : MonoBehaviour
    {
		[SerializeField] private int size;
		[SerializeField] private TMP_Text timeText;
		private int[] items;

		private void Start()
		{
			items = new int[size];
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = i * 10 + Random.Range(0,10);
			}
		}

		public void Begin()
		{
			// Create array of size
			if (items.Length != size)
			{
				items = new int[size];
				for (int i = 0; i < items.Length; i++)
				{
					items[i] = i * 10 + Random.Range(0, 10);
				}
			}

			float t = Time.realtimeSinceStartup;
			int searchScore = items[Random.Range(0, items.Length)];
			int rank = GetRank(searchScore);
			t = (Time.realtimeSinceStartup - t);
			timeText.text = t + " s";
			Debug.Log("Complexity2: Found score " + searchScore + " at position " +  rank + "/" + items.Length);
		}

		/// <summary>
		/// Finds a scores index in a leaderboard
		/// </summary>
		/// <param name="x">Cutoff</param>
		/// <returns>Number of items greater than x</returns>
		private int GetRank(int score)
		{
			
			// start searching area between all scores
			int low = 0;
			int high = items.Length - 1;

			// while our search area is > 0
			while (low <= high)
			{
				int mid = low + (high - low) / 2; // search from the mid of search area

				if (items[mid] == score) return mid; // arrived at index of our score
				
				if(items[mid] < score) // the mid score is less than our score, so skip searching lower scores
				{
					low = mid + 1;
				}
				else // the mid score is higher than our score, so skip larger scores
				{
					high = mid - 1;
				}
			}

			return -1; // Score was not in array
		}
	}
}
