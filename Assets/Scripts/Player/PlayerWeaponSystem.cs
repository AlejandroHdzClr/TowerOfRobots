using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerWeaponSystem : PlayerSystem
    {
        [SerializeField] private WeaponLogic weapon;
        [SerializeField] private float rotationSpeed;

        public event Action<bool> OnShoot;
        public event Action<Vector3> MouseHasChanged;

        private bool isReloading = false;
        private void OnEnable()
        {
            main.InputActions.Gameplay.Enable();
            main.InputActions.Gameplay.MousePosition.performed += GetMousePosition;
            main.InputActions.Gameplay.Shoot.started += ShootOnStarted;
            main.InputActions.Gameplay.Shoot.canceled += OnShootEnded;
            weapon.StopReloading += StopReloading;
            weapon.IsReloading += IsReloading;
        }
        
        private void OnDisable()
        {
            main.InputActions.Gameplay.Disable();
            main.InputActions.Gameplay.MousePosition.performed -= GetMousePosition;
            main.InputActions.Gameplay.Shoot.started -= ShootOnStarted;
            main.InputActions.Gameplay.Shoot.canceled -= OnShootEnded;
            weapon.StopReloading -= StopReloading;
            weapon.IsReloading -= IsReloading;
        }

        private void IsReloading()
        {
            isReloading = true;
        }

        private void StopReloading()
        {
            isReloading = false;
        }

        private void ShootOnStarted(InputAction.CallbackContext obj)
        {
            OnShoot?.Invoke(true);
        }private void OnShootEnded(InputAction.CallbackContext obj)
        {
            OnShoot?.Invoke(false);
        }

        private void GetMousePosition(InputAction.CallbackContext obj)
        {
            Vector2 mousePos = obj.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            Vector3 direction = worldPos - weapon.gameObject.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (worldPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                angle += 180f;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(!isReloading)
            {
                weapon.gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
            }
            MouseHasChanged?.Invoke(direction.normalized);
            
        }

        private void Update()
        {
            if(isReloading)
            {
                weapon.gameObject.transform.Rotate(Vector3.forward * (rotationSpeed*Time.deltaTime));
            }  
        }
    }
}