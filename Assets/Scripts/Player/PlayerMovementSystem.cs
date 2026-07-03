using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementSystem : PlayerSystem
    {
        [SerializeField] private float speed;
        private Vector2 movementVector;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            main.InputActions.Gameplay.Enable();
            main.InputActions.Gameplay.Move.performed += OnMove;
            main.InputActions.Gameplay.Move.canceled += OnMove;
        }
        
        private void OnDisable()
        {
            main.InputActions.Gameplay.Disable();
            main.InputActions.Gameplay.Move.performed -= OnMove;
            main.InputActions.Gameplay.Move.canceled -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            movementVector = obj.ReadValue<Vector2>();

            if (movementVector.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(movementVector.x > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        }

        private void Update()
        {
            main.transform.Translate(movementVector*(speed*Time.deltaTime));
        }
    }
}