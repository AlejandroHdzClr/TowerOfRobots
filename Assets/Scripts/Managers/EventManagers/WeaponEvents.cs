using System;
using UnityEngine;

namespace Managers.EventManagers
{
    public static class WeaponEvents
    {
        public static event Action OnReloading, OnStopReloading;
        public static event Action<bool> OnShoot;
        public static event Action<Vector3> OnMouseChanged;

        public static void Reloading()
        {
            OnReloading?.Invoke();
        }

        public static void StoppingReload()
        {
            OnStopReloading?.Invoke();
        }

        public static void Shooting(bool shoot)
        {
            OnShoot?.Invoke(shoot);
        }

        public static void ChangingMouse(Vector3 direction)
        {
            OnMouseChanged?.Invoke(direction);
        }
    }
}