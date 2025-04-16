using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<EnemySpawnPoint> spawnPoints;
}

[System.Serializable]
public class EnemySpawnPoint
{
    public GameObject enemyPrefab; // 敌人预制体
    public Vector2 position; // 生成位置
}
