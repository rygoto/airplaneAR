using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class desplayonaircraft : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI overHeadMsg;

    [System.Serializable]
    public class FlightData
    {
        public string airline_short_name;
        public string new_destination;
        public string new_origin;
        //public string unity_coordinates;
        //public string unity_heading;
    }

    private FlightData flightData; // 現在の飛行データを保持する変数

    // Start メソッドで初期化などを行うことができます
    void Start()
    {        // 情報が設定されたら自動的に ShowFlightData メソッドを呼び出す
        ShowFlightData(flightData);
    }

    public void ShowFlightData(FlightData flightData)
    {
        string flightRoute = flightData.new_origin + " to " + flightData.new_destination;
        overHeadMsg.text = flightRoute;
    }
}
