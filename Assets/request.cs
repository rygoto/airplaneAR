using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class request : MonoBehaviour
{
    public List<ObjectData> objectDataList;

    public GameObject objectPrefab;

    public class ObjectData
    {
        public Vector3 position;
        public string name;
        public Vector3 move_direction;
        public string other_info;
    }

    public string unko;



    public Slider arg1;
    public Slider arg2;



    private void Start()
    {
        float arg1value = arg1.value;
        float arg2value = arg2.value;

        StartCoroutine(SendHttpRequest(arg1value, arg2value));
    }

    private IEnumerator SendHttpRequest(float arg1, float arg2)
    {
        string url = "";

        url += "?arg1=" + arg1.ToString() + "&arg2=" + arg2.ToString();

        Dictionary<string, string> postData = new Dictionary<string, string>
        {
            { "arg1", arg1.ToString() },
            { "arg2", arg2.ToString() }
        };


        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            // 文字化けしてるから書き換えてくれ
            Debug.Log("HTTP���N�G�X�g�G���[: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("HTTP���X�|���X: " + responseText);

            try
            {
                List<ObjectData> objectDataList = JsonUtility.FromJson<List<ObjectData>>(responseText);

                foreach (ObjectData data in objectDataList)
                {
                    Debug.Log("Position: " + data.position.x + ", " + data.position.y + ", " + data.position.z);
                    Debug.Log("Name: " + data.name);
                    Debug.Log("Move Direction: " + data.move_direction.x + ", " + data.move_direction.y + ", " + data.move_direction.z);
                    Debug.Log("Other Info: " + data.other_info);
                }

                foreach (ObjectData data in objectDataList)
                {
                    GameObject newObj = Instantiate(objectPrefab);
                    Rigidbody rb = newObj.GetComponent<Rigidbody>();

                    newObj.transform.position = data.position;
                    newObj.name = data.name;

                    if (rb != null)
                    {
                        float moveSpeed = 5.0f;
                        Vector3 movedirection = data.move_direction.normalized;

                        rb.AddForce(movedirection * moveSpeed, ForceMode.VelocityChange);
                    }

                }


            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON�p�[�X�G���[: " + e.Message);
            }
        }
    }
}
