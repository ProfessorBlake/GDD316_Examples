using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Game
{
    public class InClass : MonoBehaviour
    {
		const string WEATHER_API_URL = "https://api.weather.gov/gridpoints/OKX/99,66/forecast";

		[SerializeField] private Weather weather;
		[SerializeField] private GameObject infoTemplate;

		[Serializable]
		public class Weather
		{
			public Properties properties;
		}

		[Serializable]
		public class Period
		{
			public string name;
			public int temperature;
			public ProbabilityOfPrecipitation probabilityOfPrecipitation;
			public string windSpeed;
			public string windDirection;
			public string shortForecast;
			public string detailedForecast;
		}

		[Serializable]
		public class ProbabilityOfPrecipitation
		{
			public string unitCode;
			public int value;
		}

		[Serializable]
		public class Properties
		{
			public Period[] periods;
		}

		private void Start()
		{
			StartCoroutine(GetData());
		}

		private IEnumerator GetData()
		{
			using (UnityWebRequest request = UnityWebRequest.Get(WEATHER_API_URL))
			{
				yield return request.SendWebRequest();

				if (request.result == UnityWebRequest.Result.Success)
				{ 
					string txt = request.downloadHandler.text;
					Debug.Log(txt);
					weather = JsonUtility.FromJson<Weather>(request.downloadHandler.text);
				}
			}

			foreach(Period p in weather.properties.periods)
			{
				GameObject newInfo = Instantiate(infoTemplate, infoTemplate.transform.parent);
				newInfo.SetActive(true);
				string s = $"<b><size=+10>{p.name}</size></b>\n\n" +
					$"<size=-5>{p.probabilityOfPrecipitation.value}% Chance Of Rain</size>\n" +
					$"<size=-5>Temperature of {p.temperature}°f</size>\n\n" +
					$"<i>{p.detailedForecast}</i>";
				newInfo.GetComponentInChildren<TMP_Text>().text = s; 
			}
		}
    }
}
