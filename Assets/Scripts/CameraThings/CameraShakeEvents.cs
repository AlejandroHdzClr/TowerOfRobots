using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CameraThings
{
    public class CameraShakeEvents : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;
        
        private void OnEnable()
        {
            CameraEvents.OnShakeRequested += HandleShake;
        }
        
        private void OnDisable()
        {
            CameraEvents.OnShakeRequested -= HandleShake;
        }

        private void HandleShake(float obj)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            impulseSource.GenerateImpulseWithVelocity(randomDirection*obj);
        }
    }
}