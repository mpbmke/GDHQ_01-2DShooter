using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] float _magnitude = 1f;

    public IEnumerator CameraShake()
    {
        Vector3 originalRotation = transform.localEulerAngles;
        float timeElapsed = 0.0f;

        while (timeElapsed < 0.5f)
        {
            float x = Random.Range(-1, 1) * _magnitude;
            float y = Random.Range(-1, 1) * _magnitude;
            float z = Random.Range(-1, 1) * _magnitude;

            transform.localEulerAngles = new Vector3(x, y, z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localEulerAngles = originalRotation;
    }
}
