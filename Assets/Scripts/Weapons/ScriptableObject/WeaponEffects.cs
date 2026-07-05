using UnityEngine;

public enum EffectType
{
    NumberOfBullets
}

[CreateAssetMenu(fileName = "WeaponEffects", menuName = "Scriptable Objects/Weapon/WeaponEffects")]
public class WeaponEffects : ScriptableObject
{
    public EffectType Type;
    public float Value;
}
