using System;
using Interfaces;
using UnityEngine;

namespace EnemyDrops
{
    public class ExperienceOrb : MonoBehaviour, IExperience
    {
        [SerializeField] private float experience = 5f;
        
        public float GetExperience()
        {
            return experience;
        }

        public void BeingCollected()
        {
            Destroy(gameObject);
        }
    }
}