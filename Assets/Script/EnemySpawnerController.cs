using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();

    public Enemy SpawnEnemy(Transform generatorPosition)
    {
        Enemy enemy = Instantiate(enemyList[0], transform.position, Quaternion.identity);
        enemy.target = generatorPosition;
        return enemy;
    }
}