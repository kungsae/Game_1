using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingObj : Entity
{
    public override void Attack()
    {
    }
    public override void Init()
    {
        base.Init();
    }
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        base.OnDamage(_damage, attackPos, pushPower);
    }
}
[System.Serializable]
public struct dropItems
{
    public ItemBase item;
    public float dropProbability;
    public int dropMin;
    public int dropMax;
}
