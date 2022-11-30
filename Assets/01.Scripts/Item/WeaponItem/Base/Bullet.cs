using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rig;
    public Action collisionAction;
    public TrailRenderer trail;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rig.velocity = transform.up * 50;
        StartCoroutine(DestroyBulletTime(1f));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Entity"))
        {
            collision.GetComponent<Entity>().OnDamage(100, transform.position);
            //StartCoroutine(DestroyBulletTime(0.01f));
        }
    }
    public void Fire()
    {

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
