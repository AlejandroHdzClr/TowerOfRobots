using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapons.Pooling
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField]private float bulletSpeed;
        private float damage;
        private WeaponLogic owner;
        private Rigidbody2D rb;
        private Vector3 originalPosition;

        
        public ObjectPool<BulletPool> MyPool { get; set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(WeaponLogic weapon)
        {
            owner = weapon;
            originalPosition = transform.position;
        }
        
        private void Update()
        {
            rb.linearVelocity = transform.right * (bulletSpeed);

            if ((transform.position - originalPosition).magnitude >= owner.GetMaxDistance())
            {
                owner.EndOfBullet(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if (!other.gameObject.CompareTag("Player"))
                {
                    owner.BulletOnBulletHitSomething(damageable);
                    owner.EndOfBullet(this);
                }
                
            }
        }
    }
}