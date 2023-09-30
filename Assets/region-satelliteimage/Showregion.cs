using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using TMPro;
using System.Net;

public class Showregion : MonoBehaviour
{
    private Inputfield inputfield;
    public TextMeshProUGUI locationText;
    public string apiKey;
    public string apiurl;

    [Serializable]
    public class AdressComponent
    {
        public string long_name;
        public string short_name;
        public List<string> types;
    }

    [Serializable]
    public class Result
    {
        public List<AdressComponent> address_components;
    }

    [Serializable]
    public class GeocordingResponse
    {
        public List<Result> results;
        public string status;
    }
    private void Start()
    {
        inputfield = FindObjectOfType<Inputfield>();
        //float value1 = inputfield.GetInputField1Value();
        //float value2 = inputfield.GetInputField2Value(); // Unityの任意の値を設定      
    }

    public void StartGettingRegion()
    {

        float value1 = inputfield.GetInputField1Value();
        float value2 = inputfield.GetInputField2Value(); // Unityの任意の値を設定
        float latitude = value1;
        float longitude = value2;

        string url = $"{apiurl}latlng={latitude},{longitude}&key={apiKey}";

        StartCoroutine(Getregionname(url));
    }

    IEnumerator Getregionname(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("リクエストエラー: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                GeocordingResponse geocodingResponse = JsonUtility.FromJson<GeocordingResponse>(responseText);

                if (geocodingResponse != null && geocodingResponse.results.Count > 0)
                {
                    List<AdressComponent> addressComponents = geocodingResponse.results[0].address_components;

                    string countryName = null;
                    string stateName = null;
                    string countyName = null;

                    foreach (var component in addressComponents)
                    {
                        List<string> types = component.types;
                        foreach (string type in types)
                        {
                            if (type == "administrative_area_level_1")
                            {
                                stateName = component.long_name;
                            }
                            else if (type == "administrative_area_level_2")
                            {
                                countyName = component.long_name;
                                //locationText.text = countyName + "上空";
                            }
                            else if (type == "country")
                            {
                                countryName = component.long_name;
                            }
                        }
                    }

                    string locationName = $"{countryName} {stateName} {countyName}  OverSky"; //上空
                    locationText.text = locationName;
                }
            }
        }


    }
}