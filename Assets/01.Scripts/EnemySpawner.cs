using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyLevel[] enemys;
    public int allOutEnemyCount= 0;
    public int level;
    public int levelMonsterCount;
    [SerializeField] private Transform[] spawnPoint;
    private void Awake()
    {
        for (int i = 0; i < enemys[level].enemyCount.Length; i++)
        {
            levelMonsterCount += enemys[level].enemyCount[i];
        }
    }
    public void Spawn()
    {
        int idx = RandCount();
        if (idx == -1)
        {
            return;
        }
        GameObject randEnemy = enemys[level].enemy[idx];
        Enemy enemy = PoolManager<Enemy>.instance.GetPool(randEnemy);
        enemy.gameObject.SetActive(true);
        enemy.gameObject.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
        enemy.dieEvent += () =>
        {
            levelMonsterCount--;
            if (levelMonsterCount == 0)
            {
                WaveClear();
            }
        };
    }
    public void WaveClear()
    {
        allOutEnemyCount = 0;

        for (int i = 0; i < enemys[level].enemyCount.Length; i++)
        {
            enemys[level].enemyCount[i] += 5;
        }
         for (int i = 0; i < enemys[level].enemyCount.Length; i++)
        {
            levelMonsterCount += enemys[level].enemyCount[i];
        }
    }
    public int RandCount()
    {
        int randIdx = Random.Range(0,enemys[level].enemyCount.Length - allOutEnemyCount);
        if (enemys[level].enemyCount.Length - allOutEnemyCount == 0)
        {
            return -1;
        }

        enemys[level].enemyCount[randIdx]--;
        if (enemys[level].enemyCount[randIdx] <= 0)
        {
            int temp = enemys[level].enemyCount[randIdx];
            enemys[level].enemyCount[randIdx] = enemys[level].enemyCount[enemys[level].enemyCount.Length - 1 - allOutEnemyCount];
            enemys[level].enemyCount[enemys[level].enemyCount.Length - 1 - allOutEnemyCount] = temp;
            allOutEnemyCount++;
        }
        return randIdx;
    }
    float timeTest = 0;
    private void Update()
    {
        if (timeTest > 0.1f)
        {
            timeTest = 0;
            Spawn();
        }
        else
        {
            timeTest += Time.deltaTime;
        }
    }
}
[System.Serializable]
struct EnemyLevel
{
    public int level;
    public GameObject[] enemy;
    public int[] enemyCount;
}
