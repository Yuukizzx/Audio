using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ���� UI

public class WaveManager : MonoBehaviour
{
    public GameObject startChallengeUI; // ��ʼ��ս�� UI
    public GameObject victoryUI; // ͨ�� UI
    public Transform player; // ��Ҷ���
    public Transform centerPoint; // ��ս������
    public float interactionRange = 2f; // ������Χ

    public List<Wave> waves; // Ԥ�貨��
    private int currentWave = -1; // ��ǰ���� (-1 ����δ��ʼ)

    public GameObject spawnEffectPrefab;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private bool isWaveInProgress = false;

    

    void Start()
    {
        victoryUI.SetActive(false);  // ȷ����Ϸ��ʼʱ����ͨ��UI
        startChallengeUI.SetActive(false); // Ҳȷ����ʼ UI ����
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

        // ֻ�е�ǰ���ĵ���ȫ������������û�в����ڽ����У����ܿ�ʼ��һ��
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
            Debug.LogError("û�����õ��˲������ݣ�");
            return;
        }

        currentWave++;

        if (currentWave < 5)
        {
            isWaveInProgress = true;  // ��ǵ�ǰ���ν�����
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
                Debug.LogError("����Ԥ����Ϊ�գ����鲨������", this);
                continue;
            }

            // �������ɶ���
            GameObject spawnEffect = Instantiate(spawnEffectPrefab, spawnPoint.position, Quaternion.identity);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemySpawnSound);
            yield return new WaitForSeconds(0.5f); // ȷ�����������������ɵ���

            // ���ɵ���
            GameObject enemy = Instantiate(spawnPoint.enemyPrefab, spawnPoint.position, Quaternion.identity);

            enemy.GetComponent<Enemy>().SetWaveManager(this);
            activeEnemies.Add(enemy);
        }

        // ȷ�����е���������Ϻ󣬲����������һ��
        isWaveInProgress = false;
    }

    public void EnemyDied(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
