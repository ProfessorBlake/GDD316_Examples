using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Game.API
{
	public class APIExample2 : MonoBehaviour
	{
		// Path to Ghibli api endopint
		const string API_FACTS = "https://ghibliapi.vercel.app/films";

		// To create a c# object from JSON, create a class which the data can be deserialized into
		[System.Serializable]
		public class Film
		{
			public string id ;
			public string title ;
			public string original_title ;
			public string original_title_romanised ;
			public string image ;
			public string movie_banner ;
			public string description ;
			public string director ;
			public string producer ;
			public string release_date ;
			public string running_time ;
			public string rt_score ;
			public List<string> people ;
			public List<string> species ;
			public List<string> locations ;
			public List<string> vehicles ;
			public string url ;
		}
		[System.Serializable]
		public class FilmList
		{
			public Film[] Films;
		}

		[SerializeField] private Film film;
		[SerializeField] private Image image;
		[SerializeField] private TMP_Text titleText;
		[SerializeField] private TMP_Text descriptionText;
		[SerializeField] private FilmList films;
		[SerializeField] private AudioClip sfxFilmChange;

		private void Start()
		{
			StartCoroutine(FetchDataCoroutine(API_FACTS));
		}

		public IEnumerator FetchDataCoroutine(string url)
		{
			// Get film data
			using (UnityWebRequest request = UnityWebRequest.Get(url))
			{
				yield return request.SendWebRequest();

				if (request.result == UnityWebRequest.Result.Success)
				{
					string responseAsJSON = request.downloadHandler.text;
					responseAsJSON = "{\"Films\":" + responseAsJSON + "}";
					Debug.Log(responseAsJSON);
					films = JsonUtility.FromJson<FilmList>(responseAsJSON);
				}
			}

			// loop through films
			while (true)
			{
				film = films.Films[Random.Range(0,films.Films.Length)];
				//Set text
				titleText.text = film.title;
				descriptionText.text = film.description;

				// Get flim texture
				using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(film.image))
				{
					yield return textureRequest.SendWebRequest();

					if (textureRequest.result == UnityWebRequest.Result.Success)
					{
						Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
						Sprite newSprite = Sprite.Create(
							texture,
							new Rect(0.0f, 0.0f, texture.width, texture.height),
							new Vector2(0.5f, 0.5f)
						);
						image.sprite = newSprite;
					}
				}
				AudioSource.PlayClipAtPoint(sfxFilmChange, Vector3.zero);
				yield return new WaitForSeconds(15);
			}
		}
	}
}
