using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIStatsSystem : MonoBehaviour
    {
        [SerializeField] private Image healthBarImage;

        private void OnEnable()
        {
            PlayerEvents.OnHealthChanged += PlayerHealthSystemOnHealthHasChanged;
        }
        
        private void OnDisable()
        {
            PlayerEvents.OnHealthChanged -= PlayerHealthSystemOnHealthHasChanged;
        }

        private void PlayerHealthSystemOnHealthHasChanged(float obj)
        {
            Debug.Log($"[{gameObject.name} - InstanceID: {GetInstanceID()}] healthBarImage es null: {healthBarImage == null}", this);
            healthBarImage.fillAmount = obj;
        }
    }
}