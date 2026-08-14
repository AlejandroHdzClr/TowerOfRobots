using Interfaces;
using UnityEngine;

namespace AI
{
    public class AIMovementSystem : AISystem, IPooleable
    {
        private Collider2D ownCollider;
        private Vector3 direction;

        protected override void Awake()
        {
            base.Awake();
            ownCollider = GetComponent<Collider2D>();
        }
        
        public void OnSpawn()
        {
            if (ownCollider != null)
                ownCollider.enabled = true;
            
            direction = Vector3.zero;
        }

        public void OnDespawn()
        {
            direction = Vector3.zero;
            
            if (ownCollider != null)
                ownCollider.enabled = false;
        }

        private void FixedUpdate()
        {
            // --- VECTOR HACIA EL JUGADOR ---
            Vector3 toTarget = (Main.EnemyPosition.position - transform.position).normalized;

            // --- SEPARACIÓN ULTRA-BARATA (solo 1 enemigo cercano) ---
            Collider2D nearest = Physics2D.OverlapCircle(transform.position, Main.Radius, Main.TargetLayerMask);

            Vector3 separation = Vector3.zero;

            if (nearest != null && nearest != ownCollider)
            {
                Vector3 away = transform.position - nearest.transform.position;
                separation = away.normalized * Main.SeparationWeight;
            }

            // --- DIRECCIÓN FINAL ---
            direction = toTarget + separation;

            // --- FLIP ---
            transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);

            // --- DISTANCIA REAL AL JUGADOR ---
            float distToTarget = Vector3.Distance(transform.position, Main.EnemyPosition.position);

            if (distToTarget > Main.StoppingDistance)
            {
                transform.Translate(direction.normalized * (Main.AiSpeed * Time.deltaTime));
            }
        }
    }
}