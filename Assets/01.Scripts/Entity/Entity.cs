using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Entity : MonoBehaviour
{
    public EntityData data;
    public Action dieEvent;
    public GameObject testParticle;
    protected float hp;
    protected Animator animator;
    protected SpriteRenderer spriteRender;
    protected Rigidbody2D rigid;
    protected Vector2 dir;
    protected bool isDie = false;
    protected Material origineMat;
    protected Collider2D[] cols;
    [SerializeField] protected dropItems[] items;


    protected WaitForSeconds coolDown;

    public abstract void Attack();
    public virtual void Init()
    {
        isDie = false;
        dieEvent = null;
        hp = data.maxHp;
        foreach (var item in cols)
        {
            item.enabled = true;
        }
    }
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        origineMat = spriteRender.material;
        cols = GetComponents<Collider2D>();
        Init();
    }
    private void OnEnable()
    {
        Init();
    }
    public virtual void OnDamage(float _damage,Vector2 attackPos,float pushPower = 0)
    {
        if (isDie) return;
        float totalPushP = Mathf.Clamp(pushPower - data.pushResist,0,100);
        animator.Play("Hit");
        hp -= _damage;
        //Debug.Log(gameObject.name + " : " + _damage + "���� ����");
        rigid.velocity = (-(attackPos - (Vector2)transform.position).normalized* totalPushP);
        ParticleSystem p = PoolManager<ParticleSystem>.instance.GetPool(testParticle.gameObject);
        p.transform.position = /*attackPos*/transform.position;
        p.gameObject.SetActive(true);
        p.Play();
        StartCoroutine(hitEffectTime());
        if (hp <= 0)
        {
            Die();
            isDie = true;
            rigid.velocity = (-(attackPos - (Vector2)transform.position).normalized * totalPushP)*3;
        }
    }
    public virtual void Update()
    {
        FlipX();
    }
    public void FlipX()
    {
        if (-dir.x * transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    protected virtual IEnumerator hitEffectTime()
    {
        spriteRender.material = data.hitMat;
        yield return new WaitForSeconds(0.1f);
        spriteRender.material = origineMat;
        rigid.velocity = Vector2.zero;
    }
    public virtual IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
    protected virtual void DropItem()
    {
        foreach (var item in items)
        {
            float getprobability = Random.Range(0f, 100f);
            if (getprobability < 1)
            {
                Debug.Log(getprobability);
            }
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
    public virtual void Die()
    {
        dieEvent?.Invoke();
        foreach (var item in cols)
        {
            item.enabled = false;
        }
        DropItem();
        animator.Play("Die");
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
