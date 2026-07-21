using System;
using UnityEngine;

namespace Tower
{
    public class TowerSystem : MonoBehaviour
    {
        protected TowerMain main;
        protected virtual void Awake()
        {
            main = GetComponent<TowerMain>();
        }
    }
}