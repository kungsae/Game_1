using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Entity target;
    private bool canStateChange = true;
    private State state = State.Idle;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Init()
    {
        base.Init();
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
        dir = (target.transform.position - transform.position).normalized;
        switch (state)
        {   
            case State.Idle:
                if(canStateChange)
                StartCoroutine(WaitTime(0.1f, State.Trace));
                break;
            case State.Trace:
                AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (!animInfo.IsName("Hit")|| (animInfo.IsName("Hit")&& animInfo.normalizedTime > 1f))
                {
                    rigid.velocity = dir * data.speed;
                    animator.Play("Move");
                }
                break;
            case State.Attack:
                break;
            case State.Die:
                break;
        }
    }
    public void ChangeState(State _state)
    {
        switch (_state)
        {   
            case State.Idle:
                break;
            case State.Trace:
                break;
            case State.Attack:
                break;
            case State.Die:
                break;
        }
        state = _state;
    }
    IEnumerator WaitTime(float time,State _state)
    {
        canStateChange = false;
        yield return new WaitForSeconds(time);
        ChangeState(_state);
        canStateChange = true;
    }
    public override void Attack()
    {
    }
    public override IEnumerator Wait()
    {
        yield return base.Wait();
        PoolManager<Enemy>.instance.SetPool(this);
    }
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        base.OnDamage(_damage, attackPos, pushPower);
        if(Mathf.Clamp(pushPower - data.pushResist, 0, 100)!=0)
        ChangeState(State.Idle);
    }
    public override void Die()
    {
        base.Die();
        StartCoroutine(Wait());
    }
}
public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}
