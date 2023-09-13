using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour
{
    public Camera cameraObject;
    private RaycastHit hit;
    public Text statusText;
    public CanvasGroup canvasGroup;

    public class FlightData
    {
        public string airline_short_name;
        public string new_destination;
        public string new_origin;
        //public string unity_coordinates;
        //public string unity_heading;
    }

    private void Start()
    {
        HideCanvas();
    }

    private void Display(objectinformation objectInfo)
    {
        if (statusText != null && objectInfo != null)
        {
            statusText.text = "Object Name: " + objectInfo.ObjectName +
                              " Move Speed: " + objectInfo.moveSpeed;
        }
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform parentTransform = hit.collider.gameObject.transform;
                AircraftController aircraftController = parentTransform.root.GetComponent<AircraftController>();

                if (aircraftController != null)
                {
                    AircraftController.FlightData showflight = new AircraftController.FlightData();
                    {
                        showflight.airline_short_name = aircraftController.airlineText.text;
                        showflight.new_destination = aircraftController.destinationText.text;
                        showflight.new_origin = aircraftController.originText.text;
                    }
                    Display(parentTransform.GetComponent<objectinformation>());
                    ShowCanvas();
                    Debug.Log("showflight.airline_short_name");
                    //aircraftController.flightData
                }
            }
        }
    }

    private void ShowCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
