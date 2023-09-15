using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Linq;


public class python : MonoBehaviour
{

    public GameObject aircraftPrefab;

    private string pythonServerUrl = "https://asia-northeast1-rugged-destiny-397810.cloudfunctions.net/unityresponse";  // Pythonサーバーのアドレスとポートを指定

    private Inputfield inputfield;

    private void Start()
    {
        inputfield = FindObjectOfType<Inputfield>();
    }
    public static class JsonHelper
    {
        /// <summary>
        /// 指定した string を Root オブジェクトを持たない JSON 配列と仮定してデシリアライズします。
        /// </summary>
        public static T[] FromJson<T>(string json)
        {
            // ルート要素があれば変換できるので
            // 入力されたJSONに対して(★)の行を追加する
            //
            // e.g.
            // ★ {
            // ★     "array":
            //        [
            //            ...
            //        ]
            // ★ }
            //
            string dummy_json = $"{{\"{DummyNode<T>.ROOT_NAME}\": {json}}}";

            // ダミーのルートにデシリアライズしてから中身の配列を返す
            var obj = JsonUtility.FromJson<DummyNode<T>>(dummy_json);
            return obj.array;
        }

        [Serializable]
        private struct DummyNode<T>
        {
            // 補足:
            // 処理中に一時使用する非公開クラスのため多少設計が変でも気にしない

            // JSONに付与するダミールートの名称
            public const string ROOT_NAME = nameof(array);
            // 疑似的な子要素
            public T[] array;
            // コレクション要素を指定してオブジェクトを作成する
            public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();

        }
    }


    public void StartSendingRequest()
    {
        float value1 = inputfield.GetInputField1Value();
        float value2 = inputfield.GetInputField2Value(); // Unityの任意の値を設定
        // Unityの任意の値を設定
        float latitude = value1; // 例: 緯度
        float longitude = value2; // 例: 経度
        float radius = 500f; // 例: 表示範囲

        Debug.Log("Sending request to: " + pythonServerUrl);

        // SendRequest 関数を呼び出し、値を渡す
        SendRequest(latitude, longitude, radius);
    }

    private void SendRequest(float latitude, float longitude, float radius)
    {
        string url = $"{pythonServerUrl}?lat={latitude}&lon={longitude}&r={radius}";

        UnityWebRequest www = UnityWebRequest.Get(url);

        StartCoroutine(SendRequestCoroutine(www));
    }

    IEnumerator SendRequestCoroutine(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Response: " + responseText);

            ProcessJSONResponse(responseText);
        }
    }

    [System.Serializable]
    public class FlightDataResponse
    {
        public string airline_short_name;
        public string new_destination;
        public string new_origin;
        public string unity_coordinates;
        public string unity_heading;
        // public Vector3 instantiatePosition;
        // public Vector2 forwardVector;
    }


    private void ProcessJSONResponse(string responseText)
    {
        try
        {
            FlightDataResponse[] flightDataArray = JsonHelper.FromJson<FlightDataResponse>(responseText);

            foreach (FlightDataResponse flightData in flightDataArray)
            {
                string[] coordinatedParts = flightData.unity_coordinates.Trim('(', ')').Split(',');
                // これやな↓　今までこいつは宣言だけしてた意味のない変数だ
                Vector3 instantiatePosition = new Vector3(
                    float.Parse(coordinatedParts[0]),
                    0,
                    float.Parse(coordinatedParts[1])
                );

                string[] headingParts = flightData.unity_heading.Trim('(', ')').Split(',');
                Vector2 forwardVector = new Vector2(
                    float.Parse(headingParts[0]),
                    float.Parse(headingParts[1])
                );

                GameObject aircraft = Instantiate(aircraftPrefab);
                // 航空機オブジェクトの位置と向きを設定
                aircraft.transform.position = instantiatePosition;

                float angle = Mathf.Atan2(forwardVector.y, forwardVector.x) * Mathf.Rad2Deg;

                // 指定したangleの向きにオブジェクトを回転させるQuaternionを計算
                Quaternion newRotation = Quaternion.Euler(0f, angle + 180f, -90f);

                // オブジェクトの回転を新しい回転に設定
                aircraft.transform.rotation = newRotation;

                // 速度を設定
                AircraftController aircraftController = aircraft.GetComponent<AircraftController>();
                aircraftController.speed = -0.5f; // 任意の速度を設定（この例では5.0f）

                // 航空機オブジェクトに情報を設定
                if (aircraftController != null)
                {
                    AircraftController.FlightData flightDataForAircraft = new AircraftController.FlightData();
                    {
                        flightDataForAircraft.airline_short_name = flightData.airline_short_name;
                        flightDataForAircraft.new_destination = flightData.new_destination;
                        flightDataForAircraft.new_origin = flightData.new_origin;
                    }

                    //aircraftController.SetFlightData(flightDataForAircraft);
                    aircraftController.ShowFlightData(flightDataForAircraft);
                }
                else
                {
                    Debug.LogError("Aircraft controller is null.");
                }


            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception occurred during JSON processing: " + ex.ToString());
        }
    }

}
