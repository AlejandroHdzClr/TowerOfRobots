using System;
using UnityEngine;

namespace UI.Tower
{
    public class TowerCanvasSystem : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        
        private void OnEnable()
        {
            UIEvents.OnOpenTowerCanvas += OpenTowerCanvas;
            UIEvents.OnCloseTowerCanvas += CloseTowerCanvas;
        }

        private void CloseTowerCanvas()
        {
            Time.timeScale = 1f;
            canvas.SetActive(false);
        }

        private void OpenTowerCanvas()
        {
            canvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}