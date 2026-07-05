using System;
using Interfaces;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.Pool;
using Upgrades.Weapons;
using Weapons.Pooling;
using Random = UnityEngine.Random;

namespace Weapons
{
    public enum OperationType
    {
        Sum,
        Porcen,
        Mult,
        Asign
    }
    
    public class WeaponLogic : MonoBehaviour
    {
        
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private BulletPool bullet;
        [SerializeField] private UIManager uIManager;
        
        [Header("Camera Shake")]
        [SerializeField] private float forcePerBullet = 0.1f;
        [SerializeField] private float referenceDamage = 10f;
        [SerializeField] private float maxShakeForce = 2f;
        
        public ObjectPool<BulletPool> pool { get; private set; }
        
        private PlayerWeaponSystem _playerWeaponSystem;
        private Vector3 direction;
        
        private WeaponData weaponInstance;

        private float minAngle;
        private float maxAngle;
        
        private float timeShoot;
        private float timeReloading;
        
        private bool hasShooted;
        private bool isShooting = false;
        
        private int currentAmmo;

        private void Awake()
        {
            weaponInstance = Instantiate(weaponData);
            _playerWeaponSystem = GetComponentInParent<PlayerWeaponSystem>();
            pool = new ObjectPool<BulletPool>(CreateBullet, GetBullet, ReleaseBullet);
            currentAmmo = Mathf.RoundToInt(weaponInstance.Ammo);
            minAngle = -weaponInstance.Dispersion / 2;
            maxAngle = weaponInstance.Dispersion / 2;
        }
        
        private void OnEnable()
        {
            _playerWeaponSystem.OnShoot += PlayerMainOnOnShoot;
            _playerWeaponSystem.MouseHasChanged += GetDirection;
            uIManager.GettingThisUpgrade += UIManagerOnGettingThisUpgrade;
        }

        private void OnDisable()
        {
            _playerWeaponSystem.OnShoot -= PlayerMainOnOnShoot;
            _playerWeaponSystem.MouseHasChanged -= GetDirection;
            uIManager.GettingThisUpgrade -= UIManagerOnGettingThisUpgrade;
        }
        
        private void PlayerMainOnOnShoot(bool obj)
        {
            isShooting = obj;
        }

        private void Update()
        {
            if (isShooting)
            {
                if (!hasShooted && currentAmmo >0)
                {
                    hasShooted = true;
                    for (int i = 0; i < weaponInstance.NumberOfBullets; i++)
                    {
                        pool.Get();
                    }
                    currentAmmo--;
                    
                    float totalForce = Mathf.Clamp(
                        weaponInstance.NumberOfBullets * forcePerBullet * (weaponInstance.Damage / referenceDamage),
                        0f,
                        maxShakeForce
                    );
                    CameraEventsManager.RequestShake(totalForce);
                } 
            }
            
            if (hasShooted)
            { 
                timeShoot += Time.deltaTime;
                if (timeShoot >= weaponInstance.ShootCooldown)
                { 
                    hasShooted = false; 
                    timeShoot = 0f;
                }
            }

            if (currentAmmo <= 0)
            {
                timeReloading += Time.deltaTime;
                if (timeReloading >= weaponInstance.TimeReloading)
                {
                    currentAmmo = Mathf.RoundToInt(weaponInstance.Ammo);
                    timeReloading = 0;
                }
            }
        }

        public void BulletOnBulletHitSomething(IDamageable obj)
        {
            obj.TakeDamage(weaponInstance.Damage);
        }

        private void GetDirection(Vector3 obj)
        {
            direction = obj;
        }
        
        private void ReleaseBullet(BulletPool obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void EndOfBullet(BulletPool obj)
        {
            pool.Release(obj);
        }

        private void GetBullet(BulletPool obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
            obj.ResetTrail();
            obj.transform.right = GetDispersionBullet();
            obj.Init(this);
        }

        private Vector2 GetDispersionBullet()
        {
            float angle = Random.Range(minAngle, maxAngle);
            angle *= Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            Vector2 baseDirection = direction;
    
            return new Vector2(
                baseDirection.x * cos - baseDirection.y * sin,
                baseDirection.x * sin + baseDirection.y * cos
            );
        }

        private BulletPool CreateBullet()
        {
            BulletPool copy = Instantiate(bullet, transform.position, transform.rotation);
            copy.MyPool = pool;
            return copy;
        }

        public float GetMaxDistance()
        {
            return weaponInstance.Distance;
        }
        
        private void UIManagerOnGettingThisUpgrade(WeaponUpgrade obj)
        {
            Debug.Log($"{obj.PerkToUpgrade}");
            CheckForStats(obj.PerkToUpgrade, obj.Amount, obj.Type);
        }

        private void CheckForStats(WeaponDataName stat, float change, OperationType type)
        {
            switch (stat)
            {
                case WeaponDataName.Ammo:
                    weaponInstance.Ammo = ChangeStat(weaponInstance.Ammo, change, type);
                    break;
                case WeaponDataName.Cooldown:
                    weaponInstance.ShootCooldown = ChangeStat(weaponInstance.ShootCooldown, change, type);
                    break;
                case WeaponDataName.Dispersion:
                    weaponInstance.Dispersion = ChangeStat(weaponInstance.Dispersion, change, type);
                    minAngle = -weaponInstance.Dispersion / 2;
                    maxAngle = weaponInstance.Dispersion / 2;
                    break;
                case WeaponDataName.ReloadTime:
                    weaponInstance.TimeReloading = ChangeStat(weaponInstance.TimeReloading, change, type);
                    break;
                case WeaponDataName.Damage:
                    weaponInstance.Damage = ChangeStat(weaponInstance.Damage, change, type);
                    break;
                case WeaponDataName.Distance:
                    weaponInstance.Distance = ChangeStat(weaponInstance.Distance, change, type);
                    break;
                case WeaponDataName.NumberOfBullets:
                    weaponInstance.NumberOfBullets = Mathf.RoundToInt(ChangeStat(weaponInstance.NumberOfBullets, change, type));
                    break;
            }
        }

        private float ChangeStat(float stat, float change, OperationType type)
        {
            float result;
            switch (type)
            {
                case OperationType.Sum:
                    stat += change;
                    break;
                case OperationType.Porcen:
                    result = (stat * change) / 100;
                    stat += result;
                    break;
                case OperationType.Mult:
                    stat *= change;
                    break;
                case OperationType.Asign:
                    stat = change;
                    break;
            }
            return stat;
        }
    }
}