using System;
using System.Collections.Generic;
using Player;
using UI;
using UnityEngine;
using Upgrades.Weapons;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LevelUpCanvasSystem levelUpCanvasSystem;
        [SerializeField] private PlayerExperienceSystem playerExp;
        [SerializeField] private List<UpgradeCard> list;

        public event Action OpenLevelCanvas, CloseLevelCanvas;
        public event Action<WeaponUpgrade> GettingThisUpgrade;

        private void OnEnable()
        {
            levelUpCanvasSystem.CloseCanvas += LevelUpCanvasSystemOnCloseCanvas;
            playerExp.PlayerLevelingUp += PlayerExpOnPlayerLevelingUp;
            foreach (UpgradeCard upgradeCard in list)
            {
                upgradeCard.ChoosingThisUpgrade += UpgradeCardOnChoosingThisUpgrade;
            }
        }

        private void OnDisable()
        {
            levelUpCanvasSystem.CloseCanvas -= LevelUpCanvasSystemOnCloseCanvas;
            playerExp.PlayerLevelingUp -= PlayerExpOnPlayerLevelingUp;
            foreach (UpgradeCard upgradeCard in list)
            {
                upgradeCard.ChoosingThisUpgrade -= UpgradeCardOnChoosingThisUpgrade;
            }
        }

        private void PlayerExpOnPlayerLevelingUp(int obj)
        {
            OpenLevelCanvas?.Invoke();
            playerExp.GetInputSystem().Gameplay.Disable();
            playerExp.GetInputSystem().UI.Enable();
        }
        
        private void LevelUpCanvasSystemOnCloseCanvas(GameObject obj)
        {
            playerExp.GetInputSystem().UI.Disable();
            playerExp.GetInputSystem().Gameplay.Enable();
            obj.SetActive(false);
        }
        
        private void UpgradeCardOnChoosingThisUpgrade(WeaponUpgrade obj)
        {
            GettingThisUpgrade?.Invoke(obj);
            CloseLevelCanvas?.Invoke();
        }
    }
}