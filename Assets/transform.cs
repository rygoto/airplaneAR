using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0,0,0);

    public Vector3 initialRotation = new Vector3(0,0,0);

    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 moveDirection = -transform.right;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
