using System;
using Interfaces;
using UnityEngine;

namespace AI
{
    public class AIAttackSystem : AISystem
    {
        private bool buffAplied;
        private void OnEnable()
        {
            AIEvents.OnEnemySpawn += ChangeAiDamage;
        }
        private void OnDisable()
        {
            AIEvents.OnEnemySpawn -= ChangeAiDamage;
        }

        private void ChangeAiDamage(float obj)
        {
            if (!buffAplied)
            {
                Main.EnemyDamage *= 1f + (obj * 0.1f);
                buffAplied=true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable idamageable))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    idamageable.TakeDamage(Main.EnemyDamage);
                }
            }
        }
    }
}