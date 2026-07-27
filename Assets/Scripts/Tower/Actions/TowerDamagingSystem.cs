using UnityEngine;

namespace Tower.Actions
{
    public class TowerDamagingSystem : MonoBehaviour
    {
        [SerializeField] private float damageRate;
        [SerializeField] public float time;

        public float GetDamage()
        {
            return damageRate;
        }
    }
}