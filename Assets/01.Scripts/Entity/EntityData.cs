using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    public float maxHp;
    public float damage;
    public float speed;
    public float pushResist;
    public AttackInfo[] attackInfo;
    public Material hitMat;
}
public struct AttackInfo
{
    public float powerP;
    public float distance;
    public float coolDown;
}
