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
            for (int j = 0; j < enemys[i].count; j++)
            {
                Entity enemyObj = PoolManager<Entity>.instance.GetPool(enemys[i].enemy.gameObject);
                Vector3 randPos = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius));
                enemyObj.transform.position = randPos;
                enemyObj.gameObject.SetActive(true);
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
    [HideInInspector] public List<Enemy> liveEnemy;

}
