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

        private bool upgradeSended=false;

        private void OnEnable()
        {
            levelUpCanvasSystem.CloseCanvas += LevelUpCanvasSystemOnCloseCanvas;
            PlayerEvents.OnPlayerLevelingUp += PlayerExpOnPlayerLevelingUp;
            foreach (UpgradeCard upgradeCard in list)
            {
                UIEvents.OnChoosingThisUpgrade += UpgradeCardOnChoosingThisUpgrade;
            }
        }

        private void OnDisable()
        {
            levelUpCanvasSystem.CloseCanvas -= LevelUpCanvasSystemOnCloseCanvas;
            PlayerEvents.OnPlayerLevelingUp -= PlayerExpOnPlayerLevelingUp;
            foreach (UpgradeCard upgradeCard in list)
            {
                UIEvents.OnChoosingThisUpgrade -= UpgradeCardOnChoosingThisUpgrade;
            }
        }

        private void PlayerExpOnPlayerLevelingUp(int obj)
        {
            UIEvents.OpeningLevelCanvas();
            Time.timeScale = 0f;
            playerExp.GetInputSystem().Gameplay.Disable();
            playerExp.GetInputSystem().UI.Enable();
            upgradeSended = false;
        }
        
        private void LevelUpCanvasSystemOnCloseCanvas(GameObject obj)
        {
            playerExp.GetInputSystem().UI.Disable();
            playerExp.GetInputSystem().Gameplay.Enable();
            obj.SetActive(false);
        }
        
        private void UpgradeCardOnChoosingThisUpgrade(WeaponUpgrade obj)
        {
            if (!upgradeSended)
            {
                UIEvents.GettingThisUpgrade(obj);
                Time.timeScale = 1f;
                UIEvents.ClosingLevelCanvas();
                upgradeSended = true;
            }
        }
    }
}