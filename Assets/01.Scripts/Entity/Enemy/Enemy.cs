using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Entity
{
    public Entity target;
    [SerializeField] protected EnemySkillData skillData;
    private Vector3 SpawnPoint;
    private List<Entity> attackedEntitys = new List<Entity>();
    protected bool canStateChange = true;

    public State state = State.Idle;


    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
        DoState();
    }
    public override void Init()
    {
        base.Init();
        SpawnPoint = transform.position;
        target = null;
        state = State.Idle;
    }
    public void DoState()
    {
        if (isDie||onDamage) return;
        if (target != null && target.isDie)
        {
            target = null;
            rigid.velocity = Vector2.zero;
            state = State.Idle;
        }

        switch (state)
        {
            case State.Idle:
                animator.Play("Idle");
                rigid.velocity = Vector2.zero;
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
                if (target == null) return;
                dir = (target.transform.position - transform.position).normalized;
                if (Vector2.Distance(SpawnPoint, transform.position)>=skillData.aggroOutRange)
                {
                    StartCoroutine(ChangeStateTime(0f, State.OutRange));
                }
                if (Vector2.Distance(target.transform.position, transform.position) <= skillData.data[0].distance)
                {
                    StartCoroutine(ChangeStateTime(0f, State.Attack));
                }
                else
                {
                   animator.Play("Move");
                   rigid.velocity = dir * data.speed;
                
                }
                break;
            case State.OutRange:
                dir = (SpawnPoint - transform.position).normalized;
                if (Vector2.Distance(SpawnPoint, transform.position) <= 0.1f)
                {
                    StartCoroutine(ChangeStateTime(0f, State.Idle));
                }
                else
                {
                    hp = data.maxHp;
                    animator.Play("Move");
                    rigid.velocity = dir * data.speed;
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
        //Debug.Log(_state + "?? ???? ????");
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Trace:
                break;
            case State.Attack:
                dir = (target.transform.position - transform.position).normalized;
                rigid.velocity = Vector2.zero;
                Attack();
                break;
            case State.OutRange:
                target = null;
                break;
            case State.Die:
                break;
        }

        state = _state;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || (collision.CompareTag("Entity") && collision.gameObject.layer == LayerMask.NameToLayer("Interact")))
        {
            Entity targetEntity = collision.GetComponent<Entity>();
            if (!attackedEntitys.Contains(targetEntity))
            {
                attackedEntitys.Add(targetEntity);
                targetEntity.OnDamage(data.damage * skillData.data[0].powerP, transform.position, 15);
            }
        }
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
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        base.OnDamage(_damage, attackPos, pushPower);
    }
    public override void Die()
    {
        ChangeState(State.Die);
        base.Die();
    }
    protected override IEnumerator hitEffectTime()
    {
        yield return base.hitEffectTime();
    }
    public override void Attack()
    {
        animator.Play("Attack");
        StartCoroutine(AttackProcess());
    }
    public IEnumerator AttackProcess()
    {
        while (true)
        {
            yield return null;

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            if (isDie || info.IsName("Hit"))
            {
                defaultAttackCol.enabled = false;
                attackedEntitys.Clear();
                yield break;
            } 
            if (info.normalizedTime > 1f)
            {
                defaultAttackCol.enabled = false;
                attackedEntitys.Clear();
                rigid.velocity = Vector2.zero;
                yield break;
            }
            else if (info.normalizedTime > 0.3f)
            {
                defaultAttackCol.enabled = true;
                rigid.velocity = dir * data.speed * 3f;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (SpawnPoint == Vector3.zero) SpawnPoint = transform.position;
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.back, skillData.aggroRange);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.back, skillData.data[0].distance);
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(SpawnPoint, Vector3.back, skillData.aggroOutRange);
    }


#endif
}
public enum State
{
    Idle,
    Trace,
    Attack,
    OutRange,
    Die
}