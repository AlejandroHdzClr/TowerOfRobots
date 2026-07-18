using System;
using Interfaces;
using Managers;
using UnityEngine;

namespace Tower
{
    public class TowerInteractable : MonoBehaviour, IInteractable
    {
        private bool canBeInteractable;

        private void OnEnable()
        {
            PlayerEvents.OnInteraction += Interact;
        }

        public void Interact()
        {
            if (canBeInteractable)
            {
                UIEvents.OpeningTowerCanvas();
            }
        }

        private void Awake()
        {
            canBeInteractable = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canBeInteractable = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canBeInteractable = false;
            }
        }
    }
}