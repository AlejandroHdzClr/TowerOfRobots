using System;
using EnemyDrops;
using Interfaces;
using Managers;
using Player;
using Tower.Actions;
using UnityEngine;
using UnityEngine.Pool;
using Object = System.Object;

public enum AIType
{
    Melee,
    Range
}

public class AIBase : MonoBehaviour
{
    [Header("Stats enemigo")]
    [field: SerializeField] public float EnemyMaxHealth { get; set; }
    [field: SerializeField] public float EnemyDamage { get; set; }
    [field: SerializeField] public float AiSpeed { get; private set; }
    [field: SerializeField] public float StoppingDistance { get; private set; }
    [field: SerializeField] public AIType EnemyType { get; private set; }

    [Header("PosicionDelJugador")]
    [field: SerializeField] public Transform EnemyPosition { get; set; }
    [field: SerializeField] public float Radius { get; private set; }
    
    [Header("Torre")]
    [field: SerializeField] public TowerDamagingSystem TowerDamaging { get; set; }
    
    [Header("Experiencia")]
    [field: SerializeField] public GameObject ExpOrb { get; private set; }
    
    [Header("Separacion")]
    [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
    [field: SerializeField] public float SeparationWeight { get; private set; }

    public bool imDead=false;
    public ObjectPool<AIBase> MyPool;
    public PlayerEnemySpawnSystem owner;

    public void Init(PlayerEnemySpawnSystem playerEnemySpawnSystem)
    {
        owner = playerEnemySpawnSystem;
    }
}