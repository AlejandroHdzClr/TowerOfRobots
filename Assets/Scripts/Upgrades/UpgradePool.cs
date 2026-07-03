using System;
using System.Collections.Generic;
using UnityEngine;
using Upgrades.Weapons;
using Random = UnityEngine.Random;

public class UpgradePool : MonoBehaviour
{
    [SerializeField] private List<WeaponUpgrade> weaponPool;

    public WeaponUpgrade GettingAUpgrade()
    {
        int random = Random.Range(0, weaponPool.Count);
        Debug.Log($"He escogido: {weaponPool[random].PerkToUpgrade}");
        return Instantiate(weaponPool[random]);
    }
}
