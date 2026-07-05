using System.Collections.Generic;
using UnityEngine;
using Upgrades.Weapons;

public enum WeaponDataName
{
    Ammo,
    ReloadTime,
    Cooldown,
    Dispersion,
    Damage,
    Distance,
    NumberOfBullets
}

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [field: SerializeField]public float Ammo { get; set; }
    [field: SerializeField]public float TimeReloading { get; set; }
    [field: SerializeField]public float ShootCooldown { get; set; }
    [field: SerializeField]public float Dispersion { get; set; }
    [field: SerializeField] public int NumberOfBullets { get; set; }
    [field: SerializeField]public float Damage { get; set; }
    [field: SerializeField]public float Distance { get; set; }
    [field: SerializeField]public List<WeaponUpgrade> EffectsList { get; set; }
}
