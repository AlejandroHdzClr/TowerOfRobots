using System;
using Interfaces;
using Managers;
using Player;
using UnityEngine;

public enum AIType
{
    Melee,
    Range
}

public class AIBase : MonoBehaviour, IDamageable
{
    [Header("Stats enemigo")]
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float aiSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private AIType enemyType;

    [Header("PosicionDelJugador")]
    [SerializeField] public Transform enemyPosition;
    [SerializeField] private float radius;
    
    [Header("Experiencia")]
    [SerializeField] private GameObject expOrb;
    
    [Header("Separacion")]
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float separationWeight = 1f;

    private float currentHealth;
    private bool imDead=false;
    private bool buffAplied=false;
    private Collider2D[] colliders;
    private Collider2D ownCollider;
    private Vector3 direction;

    void Awake()
    {
        currentHealth = enemyMaxHealth;
        ownCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        buffAplied = false;
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
            enemyMaxHealth *= 1f + (obj * 0.1f);
            currentHealth = enemyMaxHealth;
            enemyDamage *= 1f + (obj * 0.1f);
            buffAplied=true;
        }
    }

    private void FixedUpdate()
    {
        // --- VECTOR HACIA EL JUGADOR ---
        Vector3 toTarget = (enemyPosition.position - transform.position).normalized;

        // --- SEPARACIÓN ULTRA-BARATA (solo 1 enemigo cercano) ---
        Collider2D nearest = Physics2D.OverlapCircle(transform.position, radius, targetLayerMask);

        Vector3 separation = Vector3.zero;

        if (nearest != null && nearest != ownCollider)
        {
            Vector3 away = transform.position - nearest.transform.position;
            separation = away.normalized * separationWeight;
        }

        // --- DIRECCIÓN FINAL ---
        direction = toTarget + separation;

        // --- FLIP ---
        transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);

        // --- DISTANCIA REAL AL JUGADOR ---
        float distToTarget = Vector3.Distance(transform.position, enemyPosition.position);

        if (distToTarget > stoppingDistance)
        {
            transform.Translate(direction.normalized * (aiSpeed * Time.deltaTime));
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (!imDead)
            {
                Debug.Log("He muerto");
                Instantiate(expOrb, transform.position, transform.rotation);
                imDead = true;
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("He sido atacado \n Mi vida restante es: " + currentHealth);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable idamageable))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                idamageable.TakeDamage(enemyDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}