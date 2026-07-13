using System;

public static class TimeEvents
{
    public static event Action<float> OnCapEntered;

    public static void EnteringCap(float cap)
    {
        OnCapEntered?.Invoke(cap);
    }
}