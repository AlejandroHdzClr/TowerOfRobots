using System;
using UnityEngine;
using Weapons;

public class WeaponImage : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private WeaponLogic logic;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = logic.weaponInstance.Image;
    }
}
