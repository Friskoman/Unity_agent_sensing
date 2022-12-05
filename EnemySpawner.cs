using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] float spawnTime = 2f;

    [SerializeField] GameObject enemyPrefab;
    int numEnemies = 0;

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= spawnTime)
        {
            timer -= spawnTime;

            Vector2 circlePosition = Random.insideUnitCircle.normalized * 30f;
            Instantiate(enemyPrefab, new Vector3(circlePosition.x, 1f, circlePosition.y), Quaternion.identity).name = $"Enemy {++numEnemies}";
        }
    }
}
