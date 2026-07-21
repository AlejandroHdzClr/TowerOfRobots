using System;
using System.Collections.Generic;
using UnityEngine;
using Upgrades.Player;
using Upgrades.Weapons;

public class TowerMain : MonoBehaviour
{
    [field:SerializeField] public float Range { get; private set; }
    [field:SerializeField] public float Damage { get; private set; }
    [field:SerializeField] public float HealthPerPulse { get; private set; }
    [field:SerializeField] public float ShieldPerPulse { get; private set; }
    [field:SerializeField] public List<PlayerUpgrade> PlayerUpgrades { get; private set; }
    [field:SerializeField] public List<WeaponUpgrade> WeaponUpgrades { get; private set; }

    public bool IsInsideRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInsideRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInsideRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
