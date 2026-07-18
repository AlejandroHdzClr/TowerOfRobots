using System;

public static class PlayerEvents
{ 
    public static event Action<float> OnHealthChanged,OnExpChanged;
    public static event Action<int> OnPlayerLevelingUp;

    public static event Action OnInteraction;

    public static void Interacting()
    {
        OnInteraction?.Invoke();
    }
    
    public static void ChangingExpBar(float currentExp)
    {
        OnExpChanged?.Invoke(currentExp);
    }
    
    public static void HealthHasBeenChanged(float currentHealth)
    {
        OnHealthChanged?.Invoke(currentHealth);
    }

    public static void PlayerHasLeveledUp(int level)
    {
        OnPlayerLevelingUp?.Invoke(level);
    }
}
