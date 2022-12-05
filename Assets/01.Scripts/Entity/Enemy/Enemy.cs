using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Entity target;

    public ItemBase weapon;
    public EnemySkillData skillData;

    protected bool canStateChange = true;
    protected State state = State.Trace;
    protected State nextState = State.Idle;

    public override void Awake()
    {
        base.Awake();
        skillCoolDownSeconds = new WaitForSeconds(skillCoolDown);
        //weapon = hand.transform.GetComponentInChildren<ItemBase>();
        //weapon.player = this;
    }
    public override void Init()
    {
        base.Init();
        skillIdx = 0;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected void Start()
    {
        target = GameManager.instance.player;
    }
    public override void Update()
    {
        if (isDie) return;
        base.Update();
        DoState();
    }
    public void DoState()
    {
        AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
        switch (state)
        {   
            case State.Idle:
                if (target != null)
                {
                    StartCoroutine(ChangeStateTime(0.5f, State.Trace));
                }
                break;
            case State.Trace:
                if (target != null)
                {
                    dir = (target.transform.position - transform.position - new Vector3(0,0.75f,0)).normalized;
                    hand.transform.rotation = Quaternion.FromToRotation(Vector2.right * transform.localScale.x, dir);
                    if (!animInfo.IsName("Hit") || (animInfo.IsName("Hit") && animInfo.normalizedTime > 1f))
                    {
                        rigid.velocity = dir * data.speed;
                        animator.Play("Move");
                    }
                    if (Vector2.Distance(target.transform.position - new Vector3(0, 0.75f, 0), transform.position) <= skillData.skillData[skillIdx].distance)
                    {
                        ChangeState(State.Attack);
                    }
                }
                break;
            case State.Attack:
                if (weapon == null)
                {
                    if (animInfo.normalizedTime > 0.3f)
                    {
                        rigid.velocity = dir * data.speed * 5f;
                    }
                }

                break;
            case State.Die:
                break;
        }
    }

    //처음 스테이트 변경 할때 안에 들어갈 내용들
    public void ChangeState(State _state)
    {
        switch (_state)
        {   
            case State.Idle:
                rigid.velocity = Vector2.zero;
                break;
            case State.Trace:
                break;
            case State.Attack:
                dir = (target.transform.position - transform.position - new Vector3(0, 0.75f, 0)).normalized;
                rigid.velocity = Vector2.zero;
                Attack();
                StartCoroutine(ChangeStateTime(0.5f, State.Idle));
                break;
            case State.Die:
                break;
        }
        state = _state;
    }
    IEnumerator ChangeStateTime(float time,State _state)
    {
        canStateChange = false;
        yield return new WaitForSeconds(time);
        ChangeState(_state);
        canStateChange = true;
    }

    public override void Attack()
    {
        if (weapon == null)
        {
            animator.Play("Attack");
        }
        else
        {
            weapon.Attack();
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
        if (Mathf.Clamp(pushPower - data.pushResist, 0, 100) != 0) ;
        //ChangeState(State.Idle);
    }
    public override void Die()
    {
        base.Die();
        StartCoroutine(DisableEntity());
    }
#if UNITY_EDITOR
    public float testFloat;
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position + new Vector3(0,0.5f,0), Vector3.back, skillData.skillData[0].distance);
    }
#endif



    //다음에 보스 만들때 쓸 기능들

    [SerializeField] protected float skillCoolDown;
    protected WaitForSeconds skillCoolDownSeconds;
    protected int skillIdx = 0;
    protected bool canUseSkill = true;

    public virtual void RandomAttackIdxSet()
    {
        if (skillData.skillData.Length >= 1)
        {
            skillIdx = UnityEngine.Random.Range(1, skillData.skillData.Length);
        }
    }
}
public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}
