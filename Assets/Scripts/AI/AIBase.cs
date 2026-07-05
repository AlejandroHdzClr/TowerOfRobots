using System;
using Interfaces;
using UnityEngine;

public class AIBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float enemyHealth;
    [SerializeField] public Transform enemyPosition;
    [SerializeField] private float aiSpeed;
    [SerializeField] private GameObject expOrb;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetLayerMask; // layer de los propios enemigos
    [SerializeField] private float separationWeight = 1f; // fuerza de la separación

    private float currentHealth;
    private bool imDead=false;
    private Collider2D[] colliders;
    private Collider2D ownCollider;
    private Vector3 direction;

    void Awake()
    {
        currentHealth = enemyHealth;
        ownCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        // Dirección base: siempre hacia el objetivo
        Vector3 toTarget = (enemyPosition.position - transform.position).normalized;

        // Dirección de separación respecto a otros enemigos cercanos
        Vector3 separation = Vector3.zero;
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerMask);

        int count = 0;
        foreach (Collider2D col in colliders)
        {
            if (col == ownCollider) continue; // ignorarse a sí mismo

            Vector3 away = transform.position - col.transform.position;
            float dist = away.magnitude;

            if (dist > 0.0001f)
            {
                // cuanto más cerca está otro enemigo, más fuerte empuja
                separation += away.normalized / dist;
                count++;
            }
        }

        if (count > 0)
        {
            separation /= count;
        }

        direction = toTarget + separation.normalized * separationWeight;

        transform.Translate(direction.normalized * (aiSpeed * Time.deltaTime));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}