using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Upgrades.Weapons;
using Weapons;

namespace UI
{
    public class ShowingUpgrades : MonoBehaviour
    {
        [SerializeField] private LevelUpCanvasSystem levelUp;
        [SerializeField] private WeaponLogic logic;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private List<GameObject> upgradeCards;

        private void Awake()
        {
            throw new NotImplementedException();
        }

        private void OnEnable()
        {
            uIManager.OpenLevelCanvas += UIManagerOnOpenLevelCanvas;
        }

        private void UIManagerOnOpenLevelCanvas()
        {
            foreach (GameObject upgradeCard in upgradeCards)
            {
                upgradeCard.SetActive(true);
            }
        }
    }
}