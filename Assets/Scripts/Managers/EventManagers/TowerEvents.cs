using System;

namespace Managers.EventManagers
{
    public static class TowerEvents
    {
        public static event Action<float> OnHealPulsed;

        public static void HealingPulse(float energy)
        {
            OnHealPulsed?.Invoke(energy);
        }
    }
}