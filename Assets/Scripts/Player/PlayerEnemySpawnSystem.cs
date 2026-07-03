using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerEnemySpawnSystem : MonoBehaviour
    {
        [SerializeField] private float minRadius;
        [SerializeField] private float maxRadius;
        [SerializeField] private float time;
        [SerializeField] private AIBase prefab;
        private float currentTime;

        private void GettingVector()
        {
            float angle = Random.Range(0, 2f * Mathf.PI);
            float distance = Random.Range(minRadius, maxRadius);

            Vector2 position = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            AIBase enemy = Instantiate(prefab, position, Quaternion.identity);
            enemy.enemyPosition = transform;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                GettingVector();
                currentTime = 0;
            }
        }
    }
}