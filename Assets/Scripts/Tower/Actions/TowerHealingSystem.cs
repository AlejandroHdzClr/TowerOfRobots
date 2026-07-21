using System;
using Managers.EventManagers;
using UnityEngine;

namespace Tower.Actions
{
    public class TowerHealingSystem : TowerSystem
    {
        [SerializeField] private float timeBetweenPulse;

        private float currentTime;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (main.IsInsideRange)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeBetweenPulse)
                {
                    TowerEvents.HealingPulse(main.HealthPerPulse);
                    currentTime = 0f;
                }
            }
        }
    }
}