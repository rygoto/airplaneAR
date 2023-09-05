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
        // �^�b�`���͂����o
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // �^�b�`���W���擾
            Vector2 touchPosition = Input.GetTouch(0).position;

            // AR Raycast�����s
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.AllTypes))
            {
                // �ŏ��̏Փˏ����擾
                ARRaycastHit hit = hits[0];

                // �Փ˂���AR�I�u�W�F�N�g����ARObjectInformation�R���|�[�l���g���擾
                objectinformation objectinformation = GetComponent<objectinformation>();

                if (objectinformation != null)
                {
                    // �I�u�W�F�N�g�����e�L�X�g�ɕ\��
                    statusText.text = "Object Name: " + objectinformation.ObjectName +
                                      "Move Speed: " + objectinformation.moveSpeed;

                    // Canvas��\��
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
        // Canvas���\��
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
