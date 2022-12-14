using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnCount enemys;
    public Transform[] spawnPoints;

    public void Spawn()
    {
        
    }

}
[System.Serializable]
public struct EnemySpawnCount
{
    public Enemy enemy;
    public int count;
    [HideInInspector] public int liveCount;

}
