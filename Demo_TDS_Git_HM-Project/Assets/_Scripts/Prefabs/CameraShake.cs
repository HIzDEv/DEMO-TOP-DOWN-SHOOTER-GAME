using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

   
    public float duration, magnitude;

    public bool Shaking;
    
   

    public IEnumerator Shake()
    {

        Debug.LogError("Camera is shaking");
        Vector3 OriginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed<duration)
        {
            Shaking = true;
            float Rx = Random.Range(-1f, 1f) * magnitude;
            float RY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(OriginalPos.x+Rx, OriginalPos.y+RY, OriginalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        Shaking = false;
        transform.localPosition = OriginalPos;
    }
}
