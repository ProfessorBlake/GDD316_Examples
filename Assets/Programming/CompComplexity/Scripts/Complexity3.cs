using TMPro;
using UnityEngine;

namespace Game.Programming
{
	/// <summary>
	/// This script executes a O(n) complexity function. The time to complete
	/// scales linearly with the number of items in the array
	/// </summary>
    public class Complexity3 : MonoBehaviour
    {
		[SerializeField] private int size;
		[SerializeField] private TMP_Text timeText;
		private int[] items;

		private void Start()
		{
			items = new int[size];
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = Random.Range(0, size);
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
					items[i] = Random.Range(0, size);
				}
			}

			float t = Time.realtimeSinceStartup;
			GetLarger(Random.Range(0, size));
			t = (Time.realtimeSinceStartup - t);
			timeText.text = t + " s";
		}

		/// <summary>
		/// Finds the number of elements in the array which are greater than x
		/// </summary>
		/// <param name="x">Cutoff</param>
		/// <returns>Number of items greater than x</returns>
		private int GetLarger(int x)
		{
			int n = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] > x) n++;
			}
			Debug.Log("GetLarger than " + x + ": " + n);
			return n;
		}
	}
}
