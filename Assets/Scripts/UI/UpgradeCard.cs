using System;
using TMPro;
using UnityEngine;
using Upgrades.Weapons;

namespace UI
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private UpgradePool weaponUpgrade;
        public event Action<WeaponUpgrade> ChoosingThisUpgrade;
        private WeaponUpgrade thisUpgrade;
        private void OnEnable()
        {
            thisUpgrade = weaponUpgrade.GettingAUpgrade();
            text.text = thisUpgrade.GetDescription();
        }

        public void GettingThisUpgrade()
        {
            ChoosingThisUpgrade?.Invoke(thisUpgrade);
        }
    }
}