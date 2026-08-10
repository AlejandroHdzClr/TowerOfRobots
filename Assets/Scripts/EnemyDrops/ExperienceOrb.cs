using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.Pool;

namespace EnemyDrops
{
    public class ExperienceOrb : MonoBehaviour, IExperience
    {
        [SerializeField] private float experience = 5f;

        private PlayerEnemySpawnSystem owner;
        
        public ObjectPool<ExperienceOrb> MyPool { get; set; }

        public void Init(PlayerEnemySpawnSystem playerEnemySpawnSystem)
        {
            owner = playerEnemySpawnSystem;
        }
        
        public float GetExperience()
        {
            return experience;
        }

        public void BeingCollected()
        {
            owner.EndOfExpOrb(this);
        }
    }
}