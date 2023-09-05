using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectmove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度

    private void Start()
    {
        // オブジェクトの初期位置を取得
        Vector3 initialPosition = transform.position;

        // 移動方向（カメラからオブジェクトへのベクトル）を計算
        Vector3 moveDirection = (initialPosition - Camera.main.transform.position).normalized;

        // オブジェクトを移動させる
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = moveDirection * (-moveSpeed);
        }
    }
}
