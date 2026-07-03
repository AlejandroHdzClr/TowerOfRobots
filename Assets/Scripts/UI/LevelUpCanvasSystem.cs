using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpCanvasSystem : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private UIManager UIManager;
    public event Action<GameObject> CloseCanvas;

    private void OnEnable()
    {
        UIManager.OpenLevelCanvas+= OpenCanvas;
        UIManager.CloseLevelCanvas+= CloseMenu;
    }
    private void OnDisable()
    {
        UIManager.OpenLevelCanvas-= OpenCanvas;
        UIManager.CloseLevelCanvas-= CloseMenu;
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
