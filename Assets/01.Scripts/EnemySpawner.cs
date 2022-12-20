using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    public float radius;
    public EnemySpawnCount[] enemys;

    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            int idx = enemys[i].count - enemys[i].liveEnemy;
            for (int j = 0; j < idx; j++)
            {
                Entity enemyObj = PoolManager<Entity>.instance.GetPool(enemys[i].enemy.gameObject);
                Vector3 randPos = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius));
                enemyObj.transform.position = randPos;
                enemyObj.gameObject.SetActive(true);
                enemys[i].liveEnemy++;
                int a = i;
                enemyObj.dieEvent += () => 
                {
                    Invoke("Spawn", 60f);
                    enemys[a].liveEnemy--; 
                };
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(radius*2, radius*2, 1));
    }
#endif
}
[System.Serializable]
public struct EnemySpawnCount
{
    public Enemy enemy;
    public int count;
    [HideInInspector] public int liveEnemy;

}
