using UnityEngine;
using Weapons;

namespace Upgrades.Player
{
    public enum Perks
    {
        Energy,
        Speed,
        Exp
    }
    [CreateAssetMenu(fileName = "PlayerUpgrade", menuName = "Scriptable Objects/Upgrades/PlayerUpgrade")]
    public class PlayerUpgrade : ScriptableObject
    {
        //Son Energy, Exp, Speed
        public Perks PerkToUpgrade;
        public float Amount;
        public OperationType Type;
    }
}
