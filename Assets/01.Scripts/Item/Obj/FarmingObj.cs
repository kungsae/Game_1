using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingObj : Entity
{
    public override void Awake()
    {
        base.Awake();
    }
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
        //DropItem();
    }
    public override void Die()
    {
        base.Die();
        StartCoroutine(Wait());
    }
    public override IEnumerator Wait()
    {
        yield return base.Wait();
        PoolManager<FarmingObj>.instance.SetPool(this);
    }
    protected override void DropItem()
    {
        base.DropItem();
    }
    protected override IEnumerator hitEffectTime()
    {
        yield return base.hitEffectTime();
    }
}
