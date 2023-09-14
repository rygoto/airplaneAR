using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class googlemapiamge : MonoBehaviour
{
    public float latitude;
    public float longitude;
    private const string STATIC_MAP_URL = "https://maps.googleapis.com/maps/api/staticmap?key=AIzaSyBxIrqs4Rhnv0Zzcy9YjmzyyEzLmK0tax4&zoom=8&size=640x640&scale=2&maptype=satellite&style=element:labels|visibility:off";// Google Maps Static API URL、${APIKey}を作成したapiキーに書き換える
    private int frame = 0;
    public int frameset = 300;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetMapImage());
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        if (frame >= frameset)
        {
            StartCoroutine(GetMapImage());
            frame = 0;
        }
    }

    IEnumerator GetMapImage()
    {
        string lat = latitude.ToString();
        string lon = longitude.ToString();

        var query = "";
        query += "&center=" + "21,105";//"35.681236,139.767125";
        //UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));// centerで取得するミニマップの中央座標を設定//"35.681236,139.767125"; // 緯度経度[]
        //query += "&center=" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));// centerで取得するミニマップの中央座標を設定//"35.681236,139.767125"; // 緯度経度
        query += "&markers=color:red%7Clabel:S%7C" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));// markersで取得するミニマップの中央座標を設定//"35.681236,139.767125"; // 緯度経度
        var req = UnityWebRequestTexture.GetTexture(STATIC_MAP_URL + query);
        yield return req.SendWebRequest();

        if (req.error == null)
        {
            Destroy(GetComponent<Renderer>().material.mainTexture); //マップをなくす
            GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture; //マップを貼りつける
        }
    }
}
