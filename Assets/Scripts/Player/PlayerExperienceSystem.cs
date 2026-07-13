using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerExperienceSystem : PlayerSystem
    {
        [SerializeField, Range(0f,1f)] private float expIncremental;
        
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("ExpOrbs") && other.TryGetComponent(out IExperience experience ))
            {
                main.CurrentExperience += experience.GetExperience();
                experience.BeingCollected();
                Debug.Log("Exp recogida");
                GettingExperience();
            }
        }

        private void GettingExperience()
        {
            if (main.CurrentExperience >= main.MaxExperience)
            {
                float remainExp = main.CurrentExperience - main.MaxExperience;
                main.CurrentExperience = remainExp;
                float expIncrement = ((main.MaxExperience * expIncremental));
                main.MaxExperience += expIncrement;
                Debug.Log($"Subí de nivel, ahora necesito {main.MaxExperience}");
                main.currentLevel++;
                PlayerEvents.PlayerHasLeveledUp(main.currentLevel);
            }
            else
            {
                Debug.Log($"De exp: {main.MaxExperience} que tengo que obtener, acabo de conseguir tener {main.CurrentExperience} en total");
            }
        }
    }
}