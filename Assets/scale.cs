using UnityEngine;

public class Scale : MonoBehaviour
{
    public Camera mainCamera;
    public float minDistance = 5.0f; // UI要素のスケール変更が無効になる最小の距離
    public float maxDistance = 50.0f; // UI要素のスケール変更が無効になる最大の距離
    public float fixedDistance = 10.0f;
    public Vector3 defaultScale = Vector3.one; // UI要素のデフォルトスケール

    void Update()
    {
        if (mainCamera != null)
        {
            // カメラからの距離を計算
            float distance = Vector3.Distance(transform.position, mainCamera.transform.position);

            // 距離が一定範囲内にあるかチェック
            if (distance >= minDistance && distance <= maxDistance)
            {
                // UI要素のスケール変更を無効にしてデフォルトスケールを適用
                transform.localScale = defaultScale;
            }
            else
            {
                // カメラからの距離に応じてUI要素のスケールを調整
                float scaleFactor = distance / fixedDistance;
                transform.localScale = defaultScale * scaleFactor;
            }
        }
    }
}

//set:scale 2.2  distance 5 ->  
//next:scale 4.4  distance 10 ->

//new scale = scale * (next distance / set distance)
//minimum distance 24
//max distance 46
//default scale 0.2