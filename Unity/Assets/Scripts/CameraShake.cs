using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 currentShake;
    public void Shake(float duration, AnimationCurve curve)
    {
        StartCoroutine(DoShake(duration, curve));
    }

    private IEnumerator DoShake(float duration, AnimationCurve curve)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float strength = curve.Evaluate(time / duration);
            currentShake = (Vector2)(Random.insideUnitSphere * strength);
            yield return null;
        }

    }
}
