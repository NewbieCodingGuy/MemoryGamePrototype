using System.Collections;
using UnityEngine;

public static class Utility
{
    public static IEnumerator FlashSprite(SpriteRenderer sr, Color flashColor, float duration)
    {
        if (sr == null) yield break;
        Color original = sr.color;
        float t = 0f;
        while (t < duration)
        {
            sr.color = Color.Lerp(original, flashColor, Mathf.Sin(t * Mathf.PI * 2f / duration));
            t += Time.deltaTime;
            yield return null;
        }
        sr.color = original;
    }
}
