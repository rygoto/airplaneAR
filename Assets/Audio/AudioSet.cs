using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSet : MonoBehaviour
{
    public Transform cameraTransform;
    public AudioSource audioSource;
    private GameObject closestObject;
    public float maxDistance = 100f;

    private void Update()
    {
        closestObject = GetClosestObject(GameObject.FindGameObjectsWithTag("Aircraft"));

        if (closestObject)
        {
            float distanceToCamera = Vector3.Distance(closestObject.transform.position, cameraTransform.position);
            audioSource.volume = Mathf.Clamp01((maxDistance - distanceToCamera) / maxDistance);

            // 追加: デバッグログ
            //Debug.Log($"Closest object: {closestObject.name}, Distance: {distanceToCamera}, Volume: {audioSource.volume}");
        }
        else
        {
            Debug.Log("No aircraft objects detected.");
        }
        if (audioSource.isPlaying)
        {
            Debug.Log("Audio is playing.");
        }
        else
        {
            Debug.Log("Audio is not playing.");
        }

    }

    GameObject GetClosestObject(GameObject[] objects)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject potentialTarget in objects)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - cameraTransform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }










}
