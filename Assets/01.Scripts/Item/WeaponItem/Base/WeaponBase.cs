using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [HideInInspector] public Action attackAction;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Player player;
    [SerializeField] protected WeaponData data;
    protected WaitForSeconds coolDown;
    protected bool canAttack = true;
    protected Collider2D col;

    protected virtual void Awake()
    {
        coolDown = new WaitForSeconds(data.coolDown);
        col = gameObject.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    public virtual void Attack()
    {
        if (!canAttack) return;
            animator.Play("Attack");
            StartCoroutine(CoolDown());
    }
    public virtual void ChangeWeapon(bool get,Player _player = null)
    {
        player = _player;
        animator.Play("Idle");
        animator.enabled = get;
        StopAllCoroutines();
        canAttack = true;
        col.enabled = !get;
        if (get)
        {
            gameObject.transform.parent = _player.hand.transform;
            gameObject.layer = LayerMask.NameToLayer("Hand");
        }
        else
        {
            gameObject.transform.parent = null;
            gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
    }
    protected virtual IEnumerator CoolDown()
    {
        canAttack = false;
        yield return coolDown;
        canAttack = true;
    }
}
