using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Entity
{
    public Entity target;
    [SerializeField] private EnemySkillData skillData;

    protected bool onDamage = false;
    protected bool canStateChange = true;

    public State state = State.Idle;
    public override void Update()
    {
        base.Update();
        DoState();
    }
    public void DoState()
    {
        if (isDie||onDamage) return;
        switch (state)
        {
            case State.Idle:
                if (target != null)
                {
                    StartCoroutine(ChangeStateTime(0.5f, State.Trace));
                }
                else
                {
                    RaycastHit2D hit = Physics2D.CircleCast(transform.position, skillData.aggroRange, transform.right,0f, LayerMask.GetMask("Player"));
                    if (hit.collider != null)
                    {
                        target = hit.collider.GetComponent<Entity>();
                    }
                }
                break;
            case State.Trace:
                dir = (target.transform.position - transform.position).normalized;
                if (Vector2.Distance(target.transform.position, transform.position) <= skillData.data[0].distance)
                {
                    StartCoroutine(ChangeStateTime(0f, State.Attack));
                }
                else
                {
                    {
                        animator.Play("Move");
                        rigid.velocity = dir * data.speed;
                    }
                }
                break;
            case State.Attack:
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime > 1f)
                {
                    StartCoroutine(ChangeStateTime(0, State.Idle));
                }
                break;
            case State.Die:
                break;
        }
    }
    public void ChangeState(State _state)
    {
        Debug.Log(_state + "�� ���� ����");
        switch (_state)
        {
            case State.Idle:
                animator.Play("Idle");
                break;
            case State.Trace:
                break;
            case State.Attack:
                dir = (target.transform.position - transform.position).normalized;
                rigid.velocity = Vector2.zero;
                Attack();
                break;
            case State.Die:
                break;
        }

        state = _state;
    }
    IEnumerator ChangeStateTime(float time, State _state)
    {
        if (canStateChange)
        {
            canStateChange = false;
            yield return new WaitForSeconds(time);
            canStateChange = true;
            ChangeState(_state);
        }

    }
    public override IEnumerator DisableEntity()
    {
        yield return base.DisableEntity();
        PoolManager<Enemy>.instance.SetPool(this);
    }
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        base.OnDamage(_damage, attackPos, pushPower);
    }
    public override void Die()
    {
        base.Die();
        ChangeState(State.Die);
    }
    protected override IEnumerator hitEffectTime()
    {
        onDamage = true;
        yield return base.hitEffectTime();
        onDamage = false;
    }
    public override void Attack()
    {
        animator.Play("Attack");
        StartCoroutine(AttackProcess());
    }
    public IEnumerator AttackProcess()
    {
        Debug.Log("A");
        while (true)
        {
            yield return null;
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1f)
            {
                rigid.velocity = Vector2.zero;
                yield break;
            }
            else if (info.normalizedTime > 0.3f)
            {
                rigid.velocity = dir * data.speed * 3f;
            }

        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.back, skillData.aggroRange);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.back, skillData.data[0].distance);
    }
#endif
}
public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}