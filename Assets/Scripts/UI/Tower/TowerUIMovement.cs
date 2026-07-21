using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TowerUIMovement : MonoBehaviour
{
    [SerializeField] private PlayerSystem player;
    
    public RectTransform content;
    public float panSpeed = 1f;
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.5f;

    private bool isClicking;
    private float zoomDelta;
    private Vector2 panDelta;

    private void Awake()
    {
        content = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        player.GetInputSystem().UI.Enable();
        player.GetInputSystem().Gameplay.Disable();

        player.GetInputSystem().UI.Press.started += PressOnstarted;
        player.GetInputSystem().UI.Press.canceled += PressOncanceled;

        player.GetInputSystem().UI.Pan.performed += PanOnperformed;
        player.GetInputSystem().UI.Pan.canceled += PanOncanceled; 

        player.GetInputSystem().UI.Scroll.performed += ScrollOnperformed; 
        player.GetInputSystem().UI.Scroll.canceled += ScrollOncanceled; 
    }

    private void OnDisable()
    {
        player.GetInputSystem().UI.Press.started -= PressOnstarted;
        player.GetInputSystem().UI.Press.canceled -= PressOncanceled;

        player.GetInputSystem().UI.Pan.performed -= PanOnperformed;
        player.GetInputSystem().UI.Pan.canceled -= PanOncanceled; 

        player.GetInputSystem().UI.Scroll.performed -= ScrollOnperformed; 
        player.GetInputSystem().UI.Scroll.canceled -= ScrollOncanceled; 

        player.GetInputSystem().UI.Disable();
        player.GetInputSystem().Gameplay.Enable();
    }

    private void ScrollOncanceled(InputAction.CallbackContext obj)
    {
        zoomDelta = 0f;
    }

    private void ScrollOnperformed(InputAction.CallbackContext obj)
    {
        zoomDelta = obj.ReadValue<float>();
    }

    private void PanOncanceled(InputAction.CallbackContext obj)
    {
        panDelta = Vector2.zero;
    }

    private void PanOnperformed(InputAction.CallbackContext obj)
    {
        panDelta = obj.ReadValue<Vector2>();
    }

    private void PressOncanceled(InputAction.CallbackContext obj)
    {
        isClicking = false;
    }

    private void PressOnstarted(InputAction.CallbackContext obj)
    {
        isClicking = true;
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    void HandlePan()
    {
        if (!isClicking) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            panDelta = EventSystem.current.transform.position;
            return;
        }

        content.anchoredPosition += panDelta * panSpeed;
    }

    void HandleZoom()
    {
        if (zoomDelta == 0) return;

        float newScale = Mathf.Clamp(content.localScale.x + zoomDelta * zoomSpeed, minZoom, maxZoom);
        content.localScale = Vector3.one * newScale;
    }
}
