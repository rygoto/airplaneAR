using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARinformationdisplay : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public Text statusText;
    public CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        // タッチ入力を検出
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // タッチ座標を取得
            Vector2 touchPosition = Input.GetTouch(0).position;

            // AR Raycastを実行
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.AllTypes))
            {
                // 最初の衝突情報を取得
                ARRaycastHit hit = hits[0];

                // 衝突したARオブジェクトからARObjectInformationコンポーネントを取得
                objectinformation objectinformation = GetComponent<objectinformation>();

                if (objectinformation != null)
                {
                    // オブジェクト情報をテキストに表示
                    statusText.text = "Object Name: " + objectinformation.ObjectName +
                                      "Move Speed: " + objectinformation.moveSpeed;

                    // Canvasを表示
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
