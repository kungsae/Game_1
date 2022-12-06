using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "ScriptableObjects/EnemyAttackData", order = 1)]
public class EnemySkillData : ScriptableObject
{
    public float aggroRange;
    public Skill[] data;
}
[System.Serializable]
public struct Skill
{
    public string attackName;
    public float distance;
    public float powerP;
}
