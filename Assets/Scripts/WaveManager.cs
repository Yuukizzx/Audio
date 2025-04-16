using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 用于 UI

public class WaveManager : MonoBehaviour
{
    public GameObject startChallengeUI; // 开始挑战的 UI
    public GameObject victoryUI; // 通关 UI
    public Transform player; // 玩家对象
    public Transform centerPoint; // 挑战启动点
    public float interactionRange = 2f; // 交互范围

    public List<Wave> waves; // 预设波次
    private int currentWave = -1; // 当前波次 (-1 代表未开始)

    public GameObject spawnEffectPrefab;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private bool isWaveInProgress = false;

    

    void Start()
    {
        victoryUI.SetActive(false);  // 确保游戏开始时隐藏通关UI
        startChallengeUI.SetActive(false); // 也确保开始 UI 隐藏
        SoundManager.Instance.StopBGM();
    }

    void Update()
    {
        if (currentWave == -1 && Vector2.Distance(player.position, centerPoint.position) < interactionRange)
        {
            startChallengeUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartNextWave();
                SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmSource.clip);
                startChallengeUI.SetActive(false);
            }
        }

        // 只有当前波的敌人全部死亡，并且没有波次在进行中，才能开始下一波
        if (currentWave >= 0 && activeEnemies.Count == 0 && !isWaveInProgress && currentWave < waves.Count - 1)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        if (isWaveInProgress) return;

        if (waves == null || waves.Count == 0)
        {
            Debug.LogError("没有设置敌人波次数据！");
            return;
        }

        currentWave++;

        if (currentWave < 5)
        {
            isWaveInProgress = true;  // 标记当前波次进行中
            StartCoroutine(SpawnWave(waves[currentWave]));
        }
        else
        {
            victoryUI.SetActive(true);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.win);
            SoundManager.Instance.StopBGM();
            Time.timeScale = 0f;
            

        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        activeEnemies.Clear();

        foreach (EnemySpawnPoint spawnPoint in wave.spawnPoints)
        {
            if (spawnPoint.enemyPrefab == null)
            {
                Debug.LogError("敌人预制体为空！请检查波次配置", this);
                continue;
            }

            // 播放生成动画
            GameObject spawnEffect = Instantiate(spawnEffectPrefab, spawnPoint.position, Quaternion.identity);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemySpawnSound);
            yield return new WaitForSeconds(0.5f); // 确保动画播放完再生成敌人

            // 生成敌人
            GameObject enemy = Instantiate(spawnPoint.enemyPrefab, spawnPoint.position, Quaternion.identity);

            enemy.GetComponent<Enemy>().SetWaveManager(this);
            activeEnemies.Add(enemy);
        }

        // 确保所有敌人生成完毕后，才允许进入下一波
        isWaveInProgress = false;
    }

    public void EnemyDied(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
