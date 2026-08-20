using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TowerUIMovement : MonoBehaviour
{
    [SerializeField] private PlayerSystem player;

    private void OnEnable()
    {
        player.GetInputSystem().UI.Enable();
        player.GetInputSystem().Gameplay.Disable();

        player.GetInputSystem().UI.Interact.started += PressOnstarted;
    }

    private void OnDisable()
    {

        player.GetInputSystem().UI.Interact.started -= PressOnstarted;

        player.GetInputSystem().UI.Disable();
        player.GetInputSystem().Gameplay.Enable();
    }

    public void PressOnstarted(InputAction.CallbackContext obj)
    {
        CloseCanvas();
    }

    public void CloseCanvas()
    {
        UIEvents.ClosingTowerCanvas();
    }

}
