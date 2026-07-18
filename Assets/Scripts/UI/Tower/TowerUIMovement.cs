using System;
using Player;
using UnityEngine;

public class TowerUIMovement : MonoBehaviour
{
    [SerializeField] private PlayerSystem player;
    
    public RectTransform content;
    public float panSpeed = 1f;

    private Vector2 lastMousePos;

    private void OnEnable()
    {
        player.GetInputSystem().UI.Enable();
        player.GetInputSystem().Gameplay.Disable();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            lastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePos;
            content.anchoredPosition += delta * panSpeed;
            lastMousePos = Input.mousePosition;
        }
    }
}
