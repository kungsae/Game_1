using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingObj : Entity
{
    SpriteObj[] spriteObjs;
    [SerializeField] private dropItems[] items;
    public override void Awake()
    {
        base.Awake();
        spriteObjs = GetComponentsInChildren<SpriteObj>();
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
        DropItem();
        StartCoroutine(Wait());
    }
    public override IEnumerator Wait()
    {
        yield return base.Wait();
        PoolManager<FarmingObj>.instance.SetPool(this);
    }
    protected virtual void DropItem()
    {
        foreach (var item in items)
        {
            int getprobability = Random.Range(0, 100);
            if (item.dropProbability >= getprobability)
            {
                int itemCount = Random.Range(item.dropMin, item.dropMax);
                for (int i = 0; i < itemCount; i++)
                {
                    ItemBase getItem = PoolManager<ItemBase>.instance.GetPool(item.item.gameObject);
                    getItem.gameObject.SetActive(true);
                    getItem.DropItem(transform.position);
                }
            }
        }
    }
    protected override IEnumerator hitEffectTime()
    {
        foreach (var item in spriteObjs)
        {
            item.RenderChange(data.hitMat);
        }
        yield return base.hitEffectTime();
        foreach (var item in spriteObjs)
        {
            item.RenderChange(origineMat);
        }
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
