using UnityEngine;
using System.Linq;

public class ToggleCanvasVisibility : MonoBehaviour
{
    public string targetObjectName = "TargetName"; // 対象となるオブジェクトの名前を指定

    public void ToggleCanvas()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        var targetObjects = allObjects.Where(obj => obj.name == targetObjectName).ToArray();

        if (targetObjects.Length == 0)
        {
            Debug.LogWarning("指定された名前のオブジェクトが見つかりませんでした。");
            return;
        }

        foreach (GameObject obj in targetObjects)
        {
            Canvas canvas = obj.GetComponentInChildren<Canvas>();
            if (canvas)
            {
                canvas.enabled = !canvas.enabled;
                Debug.Log(obj.name + "のCanvasの表示状態を切り替えました: " + canvas.enabled);
            }
            else
            {
                Debug.LogWarning(obj.name + "の子オブジェクトからCanvasコンポーネントが見つかりませんでした。");
            }
        }
    }
}
