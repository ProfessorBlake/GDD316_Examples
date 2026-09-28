using TMPro;
using UnityEngine;

namespace Game.Programming
{
	/// <summary>
	/// This script executes a O(1) complexity function. The time to complete does not change
	/// based on the size of the array
	/// </summary>
    public class Complexity1 : MonoBehaviour
    {
		[SerializeField] private int size;
		[SerializeField] private TMP_Text timeText;
		private int[] items;

		public void Begin()
		{
			// Create array of size
			items = new int[size];
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = Random.Range(0, 100);
			}

			float t = Time.realtimeSinceStartup;
			GetItem(Random.Range(0, size));
			t = (Time.realtimeSinceStartup - t)*0.001f;
			timeText.text = t + " ms";
		}

		/// <summary>
		/// Gets item from array at position i
		/// </summary>
		/// <param name="i">Index of item to get</param>
		/// <returns>Int at position of i</returns>
		private int GetItem(int i)
		{
			if (i >= 0 && i < items.Length) return items[i];
			return -1;
		}
	}
}
