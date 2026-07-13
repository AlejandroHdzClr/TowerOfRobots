using System;

public static class AIEvents
{
    public static event Action<float> OnEnemySpawn;

    public static void EnemyHasBeenSpawned(float scaling)
    {
        OnEnemySpawn?.Invoke(scaling);
    }
}