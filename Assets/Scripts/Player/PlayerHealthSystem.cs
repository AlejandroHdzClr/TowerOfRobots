using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerHealthSystem : PlayerSystem, IDamageable
    {
        [SerializeField] private float damageTime;
        private float currentTime;
        private bool wasDamaged;
        protected override void Awake()
        {
            base.Awake();
            main.CurrentEnergy = main.MaxEnergy;
        }

        public void TakeDamage(float damage)
        {
            if (!wasDamaged)
            {
                main.CurrentEnergy -= damage;
                if (main.CurrentEnergy <= 0)
                {
                    Debug.Log("El player ha muerto");
                    Time.timeScale = 0f;
                }
                else
                {
                    Debug.Log($"Player hitteado, le queda {main.CurrentEnergy} de energia");
                }

                wasDamaged = true;
                currentTime = 0f;
            }
        }

        private void Update()
        {
            if (wasDamaged)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= damageTime)
                {
                    wasDamaged = false;
                    currentTime = 0f;
                }
            }
        }
    }
}