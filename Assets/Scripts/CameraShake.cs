using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float potencia, float duracion)
    {
        Vector3 originalPos = transform.localPosition;
        float lapso = 0.0f;

        while (lapso < duracion)
        {
            float x = Random.Range(-1f, 1f) * potencia;
            float y = Random.Range(-1f, 1f) * potencia;
            transform.localPosition += new Vector3(x, y, originalPos.z);
            lapso += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
