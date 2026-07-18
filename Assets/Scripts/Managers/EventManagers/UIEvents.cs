using System;
using Upgrades.Weapons;

public static class UIEvents
{
    public static event Action OnOpenLevelCanvas, OnCloseLevelCanvas, OnOpenTowerCanvas, OnCloseTowerCanvas;
    public static event Action<WeaponUpgrade> OnGettingThisUpgrade;
    public static event Action<WeaponUpgrade> OnChoosingThisUpgrade;

    public static void OpeningLevelCanvas()
    {
        OnOpenLevelCanvas?.Invoke();
    }
    public static void ClosingLevelCanvas()
    {
        OnCloseLevelCanvas?.Invoke();
    }
    public static void GettingThisUpgrade(WeaponUpgrade upgrade)
    {
        OnGettingThisUpgrade?.Invoke(upgrade);
    }
    
    public static void ChoosingThisUpgrade(WeaponUpgrade upgrade)
    {
        OnChoosingThisUpgrade?.Invoke(upgrade);
    }

    public static void OpeningTowerCanvas()
    {
        OnOpenTowerCanvas?.Invoke();
    }
    
    public static void ClosingTowerCanvas()
    {
        OnCloseTowerCanvas?.Invoke();
    }
    
}
