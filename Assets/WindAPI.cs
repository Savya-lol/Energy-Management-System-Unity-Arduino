using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[Serializable]
public class WeatherApiResponse
{
    public WeatherData[] data;
}

[Serializable]
public class WeatherData
{
    public WindInfo wind;
}

[Serializable]
public class WindInfo
{
    public string wind_dir;
    public string wind_cdir;
}

public class WindAPI : MonoBehaviour
{
    private const string apiKey = "7f97d5fee0e042f5b26649efaa9e2fe0";
    private const string apiUrl = "https://api.weatherbit.io/v2.0/current?lat=27.717245&lon=85.323959&key=" + apiKey + "&include=minutely";

    private float lastAPICallTime;
    [SerializeField] private float timeBetweenAPIRequests = 300.0f;

    [HideInInspector]public string windDir;
    [HideInInspector]public string windCdir;

    // Use this for initialization
    void Start()
    {
        lastAPICallTime = 0f;
        StartCoroutine(GetWeatherData());
    }


    IEnumerator GetWeatherData()
    {
        while (true)
        {
            if (Time.time - lastAPICallTime >= timeBetweenAPIRequests)
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        WeatherApiResponse response = JsonUtility.FromJson<WeatherApiResponse>(webRequest.downloadHandler.text);

                        if (response != null && response.data != null && response.data.Length > 0)
                        {
                            if (response.data[0].wind != null)
                            {
                                windDir = response.data[0].wind.wind_dir;

                                // Use textual representation if available, otherwise use the degree representation
                                windCdir = response.data[0].wind.wind_cdir;

                            }
                        }

                        // Print the entire API response for debugging
                        Debug.Log("API Response: " + webRequest.downloadHandler.text);

                        lastAPICallTime = Time.time;
                    }
                    else
                    {
                        Debug.LogError("Error: " + webRequest.error);
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}