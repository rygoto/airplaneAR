using UnityEngine;

public class AircraftSoundController : MonoBehaviour
{
    public Transform cameraTransform; // カメラのTransform
    public AudioClip aircraftAudioClip; // 再生する音声クリップ
    public float minVolume = 0.1f; // 最低音量
    public float maxVolume = 1f; // 最大音量
    public float maxDistance = 50f; // この距離以上だと最低音量になる

    private AudioSource currentAudioSource; // 現在音声を再生しているAudioSource

    private void Update()
    {
        GameObject closestAircraft = GetClosestObject(GameObject.FindGameObjectsWithTag("Aircraft"));

        if (closestAircraft)
        {
            if (closestAircraft != currentAudioSource?.gameObject) // 最も近いオブジェクトが変わった場合のみ
            {
                ResetAudioSource();
                currentAudioSource = closestAircraft.GetComponent<AudioSource>();
                if (!currentAudioSource)
                {
                    currentAudioSource = closestAircraft.AddComponent<AudioSource>();
                    currentAudioSource.clip = aircraftAudioClip;
                    currentAudioSource.spatialBlend = 1f; // 3D音源として設定
                    currentAudioSource.Play();
                }
            }

            float distanceToCamera = Vector3.Distance(closestAircraft.transform.position, cameraTransform.position);
            float volume = Mathf.Lerp(maxVolume, minVolume, distanceToCamera / maxDistance);
            currentAudioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
        }
        else if (currentAudioSource)
        {
            ResetAudioSource();
        }
    }

    void ResetAudioSource()
    {
        if (currentAudioSource)
        {
            currentAudioSource.Stop();
            Destroy(currentAudioSource);
        }
    }

    // カメラに最も近いオブジェクトを取得
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
