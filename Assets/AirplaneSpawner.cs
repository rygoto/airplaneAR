using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnRadius = 5.0f;

    // Update is called once per frame
    private void Update()
    {
        var angle = Random.Range(0, 360);
        var rad = angle * Mathf.Deg2Rad;
        var px = Mathf.Cos(rad) * spawnRadius;
        var pz = Mathf.Sin(rad) * spawnRadius;

        

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPoint = new Vector3(px,0,pz);
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
         
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint, rotation);

            //spawnedObject.GetComponent<Collider>().enabled = false;
        }
    }
}
