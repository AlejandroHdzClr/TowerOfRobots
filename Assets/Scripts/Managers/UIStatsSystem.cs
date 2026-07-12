using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIStatsSystem : MonoBehaviour
    {
        [SerializeField] private PlayerHealthSystem playerHealthSystem;
        [SerializeField] private Image healthBarImage;

        private void OnEnable()
        {
            playerHealthSystem.HealthHasChanged += PlayerHealthSystemOnHealthHasChanged;
        }
        
        private void OnDisable()
        {
            playerHealthSystem.HealthHasChanged -= PlayerHealthSystemOnHealthHasChanged;
        }

        private void PlayerHealthSystemOnHealthHasChanged(float obj)
        {
            Debug.Log($"[{gameObject.name} - InstanceID: {GetInstanceID()}] healthBarImage es null: {healthBarImage == null}", this);
            healthBarImage.fillAmount = obj;
        }
    }
}