using System;
using Interfaces;
using UnityEngine;

namespace AI
{
    public class AIHealthSystem : AISystem, IDamageable, IPooleable
    {
        private float currentHealth;
        private bool buffAplied;
        private bool insideTower;
        private float currentTime;

        private void OnEnable()
        {
            AIEvents.OnEnemySpawn += ChangeMaxHealth;
        }

        private void OnDisable()
        {
            AIEvents.OnEnemySpawn -= ChangeMaxHealth;
        }

        private void ChangeMaxHealth(float obj)
        {
            if (!buffAplied)
            {
                Main.EnemyMaxHealth *= 1f + (obj * 0.1f);
                currentHealth = Main.EnemyMaxHealth;
                buffAplied = true;
            }
            
        }
        
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                if (!Main.imDead)
                {
                    Debug.Log("He muerto");
                    AIEvents.LocationingDeadPosition(transform);
                    Main.imDead = true;
                    Main.owner.EndOfAiEnemy(Main);
                }
            }
            else
            {
                Debug.Log("He sido atacado \n Mi vida restante es: " + currentHealth);
            }
        }
        
        private void Update()
        {
            if (insideTower)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= Main.TowerDamaging.time)
                {
                    TakeDamage(Main.TowerDamaging.GetDamage());
                    currentTime = 0f;
                    Debug.Log("He recibido daño por la torre");
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Tower"))
            {
                Debug.Log(other.gameObject.name);
                insideTower = true;
            }
        }
         
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Tower"))
            {
                Debug.Log(other.gameObject.name);
                insideTower = false;
            }
        }

        public void OnSpawn()
        {
            Main.imDead = false;
            insideTower = false;
            buffAplied = false;
            currentTime = 0f;
            currentHealth = Main.EnemyMaxHealth;
        }

        public void OnDespawn()
        {
            insideTower = false;
            currentTime = 0f;
        }
    }
}