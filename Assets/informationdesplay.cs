using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class informationdesplay : MonoBehaviour
{
    public Camera cameraobject;
    private RaycastHit hit;
    public Text statusText;
    public CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Informationdesplay(objectinformation objectinformation)
    {
        if (statusText != null && objectinformation != null)
        {
            statusText.text = "Object Name: " + objectinformation.ObjectName +
                              "Move Speed: " + objectinformation.moveSpeed;
        }
    }


    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Button Clicked");

            Ray ray = cameraobject.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit object: " + hit.collider.gameObject.name);
                Debug.Log("Hit point: " + hit.point);

                // その他のデバッグ情報を表示
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2.0f);

                objectinformation objectinformation = hit.collider.GetComponent<objectinformation>();

                if (objectinformation != null)
                {
                    Debug.Log("Object Name: " + objectinformation.ObjectName);
                    Debug.Log("Move Speed: " + objectinformation.moveSpeed);

                    Informationdesplay(objectinformation);

                    ShowCanvas();
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
        // Canvasを非表示
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
