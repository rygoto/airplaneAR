using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour //IPointerClickHandler
{
    public string showtext;

    [System.Serializable]
    public class FlightData
    {
        public string airline_short_name;
        public string new_destination;
        public string new_origin;


        //public string unity_coordinates;
        //public string unity_heading;
    }
    //親オブジェクトの情報を取得する
    //親オブジェクトのFlightDataを取得する
    public void GetParentFlightData()
    {
        // 親オブジェクトのFlightDataを取得する
        AircraftController.FlightData flightData = transform.root.GetComponent<AircraftController.FlightData>();
        showtext = flightData.airline_short_name;
    }
}
