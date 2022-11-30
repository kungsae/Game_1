using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rig;
    public Action collisionAction;
    public TrailRenderer trail;
    private float damage = 0;
    private int a = -1;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rig.velocity = transform.up * 25;
        StartCoroutine(DestroyBulletTime(1f));
        if(a!=-1)
        a = 0;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (a <= 0)
        {
            if (collision.gameObject.CompareTag("Entity"))
            {
                if (a != -1)
                {
                    DestroyBullet();
                    ++a;
                }
                collision.GetComponent<Entity>().OnDamage(damage, transform.position);
            }
        }
    }
    public void Fire(float _damage)
    {
        trail.Clear();
        damage = _damage;
    }
    IEnumerator DestroyBulletTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyBullet();
    }
    public void DestroyBullet()
    {
        PoolManager<Bullet>.instance.SetPool(this);
    }
}
