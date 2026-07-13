using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpCanvasSystem : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public event Action<GameObject> CloseCanvas;

    private void OnEnable()
    {
        UIEvents.OnOpenLevelCanvas+= OpenCanvas;
        UIEvents.OnCloseLevelCanvas+= CloseMenu;
    }
    private void OnDisable()
    {
        UIEvents.OnOpenLevelCanvas-= OpenCanvas;
        UIEvents.OnCloseLevelCanvas-= CloseMenu;
    }

    private void OpenCanvas()
    {
        canvas.SetActive(true);
    }

    public void CloseMenu()
    {
        CloseCanvas?.Invoke(canvas.gameObject);
    }
}
