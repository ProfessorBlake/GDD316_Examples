using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.API
{
    public class APIExample1 : MonoBehaviour
    {
		// Path to Random Fact endpoint, which returns JSON object
		const string API_FACTS = "https://uselessfacts.jsph.pl//api/v2/facts/random"; 

		// To create a c# object from JSON, create a class which the data can be deserialized into
		[System.Serializable]
		public class Fact
		{
			public string id;
			public string text;
			public string source;
			public string source_url;
			public string language;
			public string permalink;
		}

		[SerializeField] private Fact fact;
		[SerializeField] private TMP_Text factText;
		[SerializeField] private AudioClip sfx;

		private int textIndex = 0;
		private float textDelay = 0;
		private bool showing;

		private void Start()
		{
			StartCoroutine(FetchDataCoroutine(API_FACTS));
		}

		public IEnumerator FetchDataCoroutine(string url)
		{
			while (true)
			{
				AudioSource.PlayClipAtPoint(sfx, Vector3.zero);
				factText.text = "";				
				yield return new WaitForSeconds(1.5f); 
				textIndex = 0;
				textDelay = 0;
				showing = true;
				using (UnityWebRequest request = UnityWebRequest.Get(url))
				{
					yield return request.SendWebRequest();

					if (request.result == UnityWebRequest.Result.Success)
					{
						string responseAsJSON = request.downloadHandler.text;
						Debug.Log(responseAsJSON);
						fact = JsonUtility.FromJson<Fact>(responseAsJSON);
					}
				}
				yield return new WaitForSeconds(14);
				showing = false;
				yield return new WaitForSeconds(5);
				fact.text = "";
			}
		}

		private void Update()
		{
			textDelay -= Time.deltaTime;
			if(textDelay <= 0)
			{
				textDelay = Random.Range(0.07f, 0.12f) * (showing ? 1f : 0.15f);
				textIndex += showing ? 1 : -1;
				textIndex = Mathf.Clamp(textIndex, 0, fact.text.Length);
				factText.text = fact.text.Substring(0, textIndex);
			}
		}
	}
}
