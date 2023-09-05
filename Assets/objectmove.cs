using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectmove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �ړ����x

    private void Start()
    {
        // �I�u�W�F�N�g�̏����ʒu���擾
        Vector3 initialPosition = transform.position;

        // �ړ������i�J��������I�u�W�F�N�g�ւ̃x�N�g���j���v�Z
        Vector3 moveDirection = (initialPosition - Camera.main.transform.position).normalized;

        // �I�u�W�F�N�g���ړ�������
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = moveDirection * (-moveSpeed);
        }
    }
}
