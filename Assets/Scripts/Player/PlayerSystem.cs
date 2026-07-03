using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSystem : MonoBehaviour
    {
        protected PlayerMain main;

        protected virtual void Awake()
        {
            main = GetComponent<PlayerMain>();
        }

        public MyInputActions GetInputSystem()
        {
            return main.InputActions;
        }
    }
}