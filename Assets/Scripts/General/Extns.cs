using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extns
{
    public static IEnumerator Tweeng(this float delay, float duration,
               System.Action<float> var, float start, float end)
    {
        yield return new WaitForSeconds(delay);
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            var(Mathf.Lerp(start, end, t));
            yield return null;
        }

        var(end);
    }

    public static IEnumerator Tweeng(this float delay, float duration,
               System.Action<Vector3> var, Vector3 start, Vector3 end)
    {
        yield return new WaitForSeconds(delay);
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT)
        {
            float t = (Time.time - sT) / duration;
            var(Vector3.Lerp(start, end, t));
            yield return null;
        }

        var(end);
    }
}
