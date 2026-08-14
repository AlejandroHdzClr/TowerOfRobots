using System;
using UnityEngine;

namespace AI
{
    public class AISystem : MonoBehaviour
    {
        protected AIBase Main;
        protected virtual void Awake()
        {
            Main = GetComponent<AIBase>();
        }
    }
}