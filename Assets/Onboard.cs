using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Onboard : MonoBehaviour
{
    private Inputfield inputfield;


    public string apiKey;
    //public float lat = 35;
    //public float lon = 139f;
    public int zoom = 8;
    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.low;
    public enum type { roadmap, satellite, gybrid, terrain };
    public type mapType = type.roadmap;
    private string url = "";
    private int mapWidth = 640;
    private int mapHeight = 640;
    private bool mapIsLoading = false; //not used. Can be used to know that the map is loading 
    private Rect rect;

    private string apiKeyLast;
    private float latLast = 35f;
    private float lonLast = 139f;
    private int zoomLast = 8;
    private resolution mapResolutionLast = resolution.low;
    private type mapTypeLast = type.roadmap;
    private bool updateMap = true;


    // Start is called before the first frame update
    void Start()
    {
        inputfield = FindObjectOfType<Inputfield>();

        StartCoroutine(GetGoogleMap());
        //rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        //mapWidth = (int)Math.Round(rect.width);
        //mapHeight = (int)Math.Round(rect.height);
    }

    // Update is called once per frame
    public void StartGettingGoogleMap()
    {
        float value1 = inputfield.GetInputField1Value();
        float value2 = inputfield.GetInputField2Value();
        float lat = value1;
        float lon = value2;
        if (updateMap && (apiKeyLast != apiKey || !Mathf.Approximately(latLast, lat) || !Mathf.Approximately(lonLast, lon) || zoomLast != zoom || mapResolutionLast != mapResolution || mapTypeLast != mapType))
        {
            //rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
            //mapWidth = (int)Math.Round(rect.width);
            //mapHeight = (int)Math.Round(rect.height);
            StartCoroutine(GetGoogleMap());
            updateMap = false;
        }
    }


    IEnumerator GetGoogleMap()
    {
        float value1 = inputfield.GetInputField1Value();
        float value2 = inputfield.GetInputField2Value();
        float lat = value1;
        float lon = value2;
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + mapResolution + "&maptype=" + mapType + "&key=" + apiKey;
        mapIsLoading = true;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            mapIsLoading = false;
            GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            apiKeyLast = apiKey;
            latLast = lat;
            lonLast = lon;
            zoomLast = zoom;
            mapResolutionLast = mapResolution;
            mapTypeLast = mapType;
            updateMap = true;
        }
    }

}