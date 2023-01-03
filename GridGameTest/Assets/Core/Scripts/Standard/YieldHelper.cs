using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class YieldHelper
{
    public static WaitForEndOfFrame WaitEndOfFrame = new WaitForEndOfFrame();

    public static IEnumerator WaitForSeconds(float seconds, bool ignoreTimeScale = false)
    {
        float t = 0;

        while (t < seconds)
        {
            yield return null;

            t += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        }
    }

    public static IEnumerator WaitForFrame(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
    }

    public static IEnumerator WaitUntil(Func<bool> predicate)
    {
        while (predicate.Invoke() == false)
        {
            yield return null;
        }
    }

    public static IEnumerator WaitWhile(Func<bool> predicate)
    {
        while (predicate.Invoke())
        {
            yield return null;
        }
    }

    public static IEnumerator WaitUntilSafe(Func<bool> predicate, float maxWaitTime)
    {
        float currentWaitTime = 0f;

        while (predicate.Invoke() == false && currentWaitTime < maxWaitTime)
        {
            currentWaitTime += Time.deltaTime;

            yield return null;
        }
    }

    public static IEnumerator WaitWhileSafe(Func<bool> predicate, float maxWaitTime)
    {
        float currentWaitTime = 0f;

        while (predicate.Invoke() && currentWaitTime < maxWaitTime)
        {
            currentWaitTime += Time.deltaTime;

            yield return null;
        }
    }
}