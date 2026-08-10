using System;
using UnityEngine;

public static class AIEvents
{
    public static event Action<float> OnEnemySpawn;
    public static event Action<Transform> OnLocationDead;

    public static void LocationingDeadPosition(Transform transform)
    {
        OnLocationDead?.Invoke(transform);
    }

    public static void EnemyHasBeenSpawned(float scaling)
    {
        OnEnemySpawn?.Invoke(scaling);
    }
}