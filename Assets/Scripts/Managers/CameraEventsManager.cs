using System;
using UnityEngine;

public static class CameraEventsManager
{
    public static event Action<float> OnShakeRequested;

    public static void RequestShake(float force)
    {
        OnShakeRequested?.Invoke(force);
    }
}
