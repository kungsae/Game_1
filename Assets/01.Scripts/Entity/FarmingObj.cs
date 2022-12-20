using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingObj : Entity
{
    public RecoverObj obj;

    public void RecoverObjSet(RecoverObj _obj)
    {
        obj = _obj;
    }
    public override void Attack()
    {
    }
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        animator.Play("Hit");
        base.OnDamage(_damage, attackPos, pushPower);
    }
    public override void Die()
    {
        base.Die();
        obj.Recover();
    }
}
