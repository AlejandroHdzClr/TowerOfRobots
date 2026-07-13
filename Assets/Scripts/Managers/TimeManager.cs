using System;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float capTime;
        private float currentTime;
        private float maxTime;
        private float scale;

        private void Update()
        {
            currentTime += Time.deltaTime;
            maxTime += Time.deltaTime;

            if (currentTime >= capTime)
            {
                scale = 0.02f + (maxTime * 0.0005f);
                TimeEvents.EnteringCap(scale);
                currentTime = 0;
            }
        }

    }
}