using System;
using UnityEngine;
using System.Collections;

public static class InputHandler
{
    public static event Action<Spot> OnSpotClicked;
    private static bool inputEnabled = true;

    public static void NotifySpotClicked(Spot spot)
    {
        if (!inputEnabled) return;
        OnSpotClicked?.Invoke(spot);
    }

    public static void DisableInputTemporarily(float seconds = 0.2f)
    {
        GameController.Instance?.StartCoroutine(DisableRoutine(seconds));
    }

    private static IEnumerator DisableRoutine(float seconds)
    {
        inputEnabled = false;
        yield return new WaitForSeconds(seconds);
        inputEnabled = true;
    }
}
