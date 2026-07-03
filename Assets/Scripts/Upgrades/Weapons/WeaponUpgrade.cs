using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Upgrades.Weapons
{
    [CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Scriptable Objects/Upgrades/WeaponUpgrade")]
    public class WeaponUpgrade : ScriptableObject
    {
        public WeaponDataName PerkToUpgrade;
        public float Amount;
        public OperationType Type;
        public List<WeaponEffects> Effects;

        public string GetDescription()
        {
            string operation="";
            switch (Type)
            {
                case OperationType.Sum:
                    operation = " + ";
                    break;
                case OperationType.Porcen:
                    operation = " % ";
                    break;
                case OperationType.Mult:
                    operation = " * ";
                    break;
                case OperationType.Asign:
                    operation = " = ";
                    break;
            }

            return $"{PerkToUpgrade} {operation} {Amount}";
        }
    }
}