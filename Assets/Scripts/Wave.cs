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
    public GameObject enemyPrefab; // ����Ԥ����
    public Vector2 position; // ����λ��
}
