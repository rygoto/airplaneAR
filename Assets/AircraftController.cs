using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AircraftController : MonoBehaviour
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

    public TextMeshProUGUI airlineText; // 航空会社のテキストUI
    public TextMeshProUGUI destinationText; // 目的地のテキストUI
    public TextMeshProUGUI originText; // 出発地のテキストUI

    public float speed = -0.5f; // 航空機の速度

    private void Update()
    {
        // 航空機を前進させる
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetFlightData(FlightData flightData)
    {
        // 航空機の情報をUIに表示
        airlineText.text = "Airline: " + (flightData.airline_short_name ?? "N/A");
        destinationText.text = "Destination: " + (flightData.new_destination ?? "N/A");
        originText.text = "Origin: " + (flightData.new_origin ?? "N/A");
    }

    public void ShowFlightData(FlightData flightData)
    {
        string flightRoute = flightData.new_origin + " to " + flightData.new_destination;
        overHeadMsg.text = flightRoute;
    }

}
