using System;
using EnemyDrops;
using Interfaces;
using Managers;
using Tower.Actions;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerEnemySpawnSystem : MonoBehaviour
    {
        [SerializeField] private float minRadius;
        [SerializeField] private float maxRadius;
        [SerializeField] private float time;
        [SerializeField] private AIBase prefab;
        [SerializeField] private float scale;
        [SerializeField] private TowerDamagingSystem damageSystem;
        [SerializeField] private ExperienceOrb expOrb;
        private float currentTime;
        private Transform lastAiPosition;
        
        public ObjectPool<AIBase> AiPool { get; private set; }
        public ObjectPool<ExperienceOrb> ExpPool { get; private set; }


        private void OnEnable()
        {
            TimeEvents.OnCapEntered += TimeChange;
            AIEvents.OnLocationDead += AIEventsOnOnLocationDead;
        }

        private void Awake()
        {
            ExpPool = new ObjectPool<ExperienceOrb>(CreateExpOrb, GetExpOrb, ReleaseExpOrb);
            AiPool = new ObjectPool<AIBase>(CreateAiEnemy, GetAiEnemy, ReleaseAiEnemy);
        }

        public void EndOfAiEnemy(AIBase enemy)
        {
            AiPool.Release(enemy);
        }
        
        private void ReleaseAiEnemy(AIBase obj)
        {
            obj.gameObject.SetActive(false);
            foreach (IPooleable pooleable in obj.GetComponents<IPooleable>())
            {
                pooleable.OnDespawn();
            }
        }

        private void GetAiEnemy(AIBase obj)
        {
            obj.gameObject.SetActive(true);
            obj.Init(this);
            foreach (IPooleable pooleable in obj.GetComponents<IPooleable>())
            {
                pooleable.OnSpawn();
            }
            Debug.Log("Get AiEnemy Init");
        }

        private AIBase CreateAiEnemy()
        {
            AIBase copy = Instantiate(prefab);
            copy.MyPool = AiPool;
            return copy;
        }

        private void ReleaseExpOrb(ExperienceOrb orb)
        {
            orb.gameObject.SetActive(false);
        }

        private void GetExpOrb(ExperienceOrb orb)
        {
            orb.gameObject.SetActive(true);
            orb.Init(this);
            orb.transform.position = lastAiPosition.position;
        }

        public void EndOfExpOrb(ExperienceOrb orb)
        {
            ExpPool.Release(orb);
        }
        
        private ExperienceOrb CreateExpOrb()
        {
            ExperienceOrb copy = Instantiate(expOrb, lastAiPosition.position, lastAiPosition.rotation);
            copy.MyPool = ExpPool;
            return copy;
        }

        private void AIEventsOnOnLocationDead(Transform transform)
        {
            lastAiPosition = transform;
            ExpPool.Get();
        }

        private void TimeChange(float obj)
        {
            scale += obj;
            
            if (time > 0.1f)
            {
                time -= obj;
            }

            if (time < 0.1f)
            {
                time = 0.1f;
            }
        }

        private void GettingVector()
        {
            float angle = Random.Range(0, 2f * Mathf.PI);
            float distance = Random.Range(minRadius, maxRadius);

            Vector2 position = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            
            AIBase enemy = AiPool.Get();
            enemy.transform.position = position;
            enemy.EnemyPosition = transform;
            enemy.TowerDamaging = damageSystem;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                GettingVector();
                AIEvents.EnemyHasBeenSpawned(scale);
                currentTime = 0;
            }
        }
    }
}